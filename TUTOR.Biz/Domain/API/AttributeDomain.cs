using TUTOR.Biz.Domain.API.Interface;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Extensions;
using TUTOR.Biz.Helpers;
using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.API
{
    public class AttributeDomain : IAttributeDomain, IAdminApiDomain
    {
        private readonly ITokenRepository tokenRepo;

        private readonly IHttpContextAccessor httpContextAccessor;

        public AttributeDomain(ITokenRepository tokenRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.tokenRepo = tokenRepo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool VerifyToken(string token, string clientIp)
        {
            bool result;
            var dto = tokenRepo.Get(token, clientIp);
            if (dto == null)
            {
                result = false;
            }
            else
            {
                var ip = httpContextAccessor.HttpContext.GetIP();
                if (!string.IsNullOrEmpty(dto.IP) && ip != dto.IP)
                {
                    result = false;
                }
                else
                {
                    if (dto.StartDate.HasValue && dto.EndDate.HasValue)
                    {
                        var now = DateTimeHelper.TaipeiNow;
                        if (!(dto.StartDate <= now && dto.EndDate >= now))
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public void TokenLog(string token)
        {
            var ip = httpContextAccessor.HttpContext.GetIP();
            var dto = tokenRepo.Get(token, ip);
            if (dto != null)
            {
                var dtoLog = new TokenLogDTO
                {
                    Token = dto.Token,
                    IP = httpContextAccessor.HttpContext.GetIP(),
                    RequestUrl = httpContextAccessor.HttpContext.GetAbsoluteUri().AbsoluteUri,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    LogDate = DateTimeHelper.TaipeiNow
                };

                tokenRepo.Log(dtoLog);
            }
        }
    }
}