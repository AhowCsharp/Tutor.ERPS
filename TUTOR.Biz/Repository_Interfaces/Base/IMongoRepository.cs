using TUTOR.Biz.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TUTOR.Biz.Repository_Interfaces.Base
{
    public interface IMongoRepository<TDTO>
        where TDTO : IDTO
    {
        Task<TDTO> InsertAsync(TDTO dto);

        Task<IEnumerable<TDTO>> GetListAsync();

        Task<TDTO> GetAsync(string id);

        Task DeleteAsync(string id);

        Task<IEnumerable<TDTO>> GetListAsync(Expression<Func<TDTO, bool>> predicate);
    }
}