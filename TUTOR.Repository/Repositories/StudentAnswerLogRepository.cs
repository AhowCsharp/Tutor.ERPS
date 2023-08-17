using AutoMapper;
using TUTOR.Repository.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Repository_Interfaces;
using TUTOR.Repository.EF;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Collections;

namespace TUTOR.Repository.Repositories
{
    public class StudentAnswerLogRepository : EFRepositoryBase<StudentAnswerLog, StudentAnswerLogDTO, int>, IStudentAnswerLogRepository
    {
        public StudentAnswerLogRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<IEnumerable<StudentAnswerLogDTO>> GetStudentAnswerLogsAsync(int studentId)
        {
            var data = await _context.StudentAnswerLog.Where(x => x.StudentId == studentId).ToListAsync();

            return _mapper.Map<IEnumerable<StudentAnswerLogDTO>>(data);
        }
    }
}