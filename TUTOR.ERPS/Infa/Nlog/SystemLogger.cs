using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Helpers;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using TUTOR.Biz.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TUTOR.ERPS.Api.Infra.Nlog
{
    public class SystemLogger
    {
        private readonly ILogSystemRepository _logSystemRepository;
        private readonly CurrentUser _currentUser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SystemLogger(ILogSystemRepository logSystemRepository, UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _logSystemRepository = logSystemRepository;
            _currentUser = userService.GetCurrentUser();
            _httpContextAccessor = httpContextAccessor;
        }

        public void Log(string level, string message, string errorMessage = null, Exception ex = null, string caller = null)
        {
            LogSystemDTO log = new();
            log.Application = "linetag.admin";
            log.Logged = DateTimeHelper.TaipeiNow;
            log.Level = level;
            log.Message = message;
            log.OfficialAccountId = _currentUser?.OfficialAccountId;
            log.UserName = _currentUser?.Name;
            log.ServerName = _httpContextAccessor.HttpContext.GetServerVariable("HTTP_HOST");
            log.Port = _httpContextAccessor.HttpContext.Request.Host.Port?.ToString();
            log.Url = _httpContextAccessor.HttpContext.GetServerVariable("HTTP_URL");
            log.Https = _httpContextAccessor.HttpContext.Request.IsHttps;
            log.ServerAddress = _httpContextAccessor.HttpContext.GetServerVariable("LOCAL_ADDR");
            log.RemoteAddress = $"{_httpContextAccessor.HttpContext.GetServerVariable("REMOTE_ADDR")}:{_httpContextAccessor.HttpContext.GetServerVariable("REMOTE_PORT")}";
            log.Callsite = caller;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                log.Exception = errorMessage;
            }
            else if (ex != null)
            {
                log.Exception = ex.ToString();
            }

            _logSystemRepository.Log(log);
        }

        public void Error(string message, Exception ex = null, [CallerMemberName] string caller = null)
        {
            Log(nameof(Error).ToUpper(), message, ex: ex, caller: caller);
        }

        public void Warn(string message, string errorMessage = null, Exception ex = null, [CallerMemberName] string caller = null)
        {
            Log(nameof(Warn).ToUpper(), message, errorMessage: errorMessage, caller: caller);
        }

        public void Info(string message, [CallerMemberName] string caller = null)
        {
            Log(nameof(Info).ToUpper(), message, caller: caller);
        }

        public void Trace(string message, [CallerMemberName] string caller = null)
        {
            Log(nameof(Trace).ToUpper(), message, caller: caller);
        }
    }
}