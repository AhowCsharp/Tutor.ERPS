using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Models;

namespace TUTOR.Biz.Domain.API.Interface
{
    public interface IMemberDomain
    {
        Task<StatusResponse> ImportFromExcel(IFormFile file);
    }
}