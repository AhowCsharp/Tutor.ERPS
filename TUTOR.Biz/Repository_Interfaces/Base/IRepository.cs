using TUTOR.Biz.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Repository_Interfaces.Base
{
    public interface IDIYRepository<TDTO, TIdentity>
        where TDTO : IDTO
    {
        Task<TDTO> InsertAsync(TDTO dto);

        Task<TDTO> GetAsync(TIdentity id);

        Task UpdateAsync(TDTO dto);

        Task DeleteAsync(TIdentity id);
    }

    public interface IRepository<TDTO>
        where TDTO : IDTO
    {
        Task<TDTO> InsertAsync(TDTO dto);

        Task<TDTO> GetAsync(Expression<Func<TDTO, bool>> predicate);

        Task DeleteAsync(Expression<Func<TDTO, bool>> predicate);
    }

    public interface IRepository<TDTO, TIdentity> : IRepository<TDTO>, IFilterRepository<TDTO>, IPagedRepository<TDTO>
        where TDTO : IDTO
    {
        Task UpdateAsync(TDTO dto);

        Task<TDTO> GetAsync(TIdentity id);

        Task DeleteAsync(TIdentity id);
    }
}