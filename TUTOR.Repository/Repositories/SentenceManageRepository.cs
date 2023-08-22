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
    public class SentenceManageRepository : EFRepositoryBase<SentenceManage, SentenceManageDTO, int>, ISentenceManageRepository
    {
        public SentenceManageRepository(IMapper mapper, TUTORERPSContext context) : base(mapper, context)
        {
        }

        public async Task<IEnumerable<SentenceManageDTO>> GetSentenceManageListAsync()
        {
            var data = await _context.SentenceManage.OrderBy(x => x.QuestionTypeId).ToListAsync();

            return data.Select(_mapper.Map<SentenceManageDTO>);
        }

        public async Task<SentenceManageDTO> GetSentenceManageAsync(int questionTypeId,int questionNum)
        {
            var data = await _context.SentenceManage.FirstOrDefaultAsync(x => x.QuestionTypeId == questionTypeId && x.QuestionNum == questionNum);

            return _mapper.Map<SentenceManageDTO>(data);
        }



        public async Task<IEnumerable<SentenceDTO>> GetSentenceListAsync(int typeId, int? questionNum)
        {
            IQueryable<SentenceManage> query = _context.SentenceManage.Where(x => x.QuestionTypeId == typeId);

            if (questionNum.HasValue)
            {
                query = query.OrderBy(x => x.QuestionNum < questionNum ? 1 : 0).ThenBy(x => x.QuestionNum);
            }
            else
            {
                query = query.OrderBy(x => x.QuestionNum);
            }

            var data = await query.ToListAsync();

            return data.Select(_mapper.Map<SentenceDTO>);
        }

        public async Task<IEnumerable<SentenceTypeDTO>> GetSentenceTypeListAsync()
        {
            var data = await _context.SentenceType.ToListAsync();

            return data.Select(_mapper.Map<SentenceTypeDTO>);
        }

        public async Task<SentenceTypeDTO> GetSentenceTypeAsync(string type)
        {
            var data = await _context.SentenceType.FirstOrDefaultAsync(x => x.Type == type);

            return _mapper.Map<SentenceTypeDTO>(data);
        }

        public async Task<bool> CreateSentenceAsync(SentenceManageDTO sentenceManageDTO)
        {
            var data = _mapper.Map<SentenceManage>(sentenceManageDTO);
            try
            {
                var exsit = _context.SentenceManage.Any(x => x.Mp3FileName == data.Mp3FileName);
                if (exsit)
                {
                    return false;
                }
                var maxNum = _context.SentenceManage.Where(x => x.QuestionTypeId == data.QuestionTypeId).Max(x => (int?)x.QuestionNum) ?? 0;
                data.QuestionNum = maxNum + 1;
                await _context.SentenceManage.AddAsync(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateSentenceAsync(SentenceManageDTO sentenceManageDTO)
        {
            try
            {
                // 从数据库中获取现有的实体
                var existingEntity = await _context.SentenceManage.FindAsync(sentenceManageDTO.id); // assuming Id is the primary key

                if (existingEntity == null)
                {
                    // 如果没有找到现有的实体，可能需要返回false或抛出异常。
                    return false;
                }

                // 使用AutoMapper将DTO的更改应用到现有的实体
                _mapper.Map(sentenceManageDTO, existingEntity);

                // 更新实体
                _context.SentenceManage.Update(existingEntity);

                // 保存更改到数据库
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // 处理或记录异常
                return false;
            }
        }


        public async Task<bool> CreateTypeAsync(SentenceTypeDTO sentenceTypeDTO)
        {
            var data = _mapper.Map<SentenceType>(sentenceTypeDTO);
            try
            {
                var exsit = _context.SentenceType.Any(x => x.Type == data.Type);
                if (exsit)
                {
                    return false;
                }
                await _context.SentenceType.AddAsync(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<bool> RemoveTypeAsync(int id)
        {
            try
            {
                var exsit = _context.SentenceType.SingleOrDefault(x => x.Id == id);
                if (exsit == null)
                {
                    return false;
                }
                _context.SentenceType.Remove(exsit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveSentenceAsync(int id)
        {
            try
            {
                var exsit = _context.SentenceManage.SingleOrDefault(x => x.Id == id);
                if (exsit == null)
                {
                    return false;
                }
                _context.SentenceManage.Remove(exsit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}