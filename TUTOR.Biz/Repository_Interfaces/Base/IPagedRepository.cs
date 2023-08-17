using TUTOR.Biz.SeedWork;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces.Base
{
    public interface IPagedRepository<TDTO>
        where TDTO : IDTO
    {
        Task<IPagedList<TDTO>> GetListAsync(Expression<Func<TDTO, bool>> predicate, string kind, string orderBy, int page, int? pageSize);
    }
}