using TUTOR.Biz.Domain.DTO;
using System.Threading.Tasks;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface ILogSystemRepository
    {
        Task LogAsync(LogSystemDTO dto);

        void Log(LogSystemDTO dto);
    }
}