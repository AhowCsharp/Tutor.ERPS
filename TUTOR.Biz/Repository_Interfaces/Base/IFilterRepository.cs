using TUTOR.Biz.SeedWork;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TUTOR.Biz.Repository_Interfaces.Base
{
    public interface IFilterRepository<TDTO>
        where TDTO : IDTO
    {
        Task<bool> AnyAsync(Expression<Func<TDTO, bool>> predicate);
    }
}