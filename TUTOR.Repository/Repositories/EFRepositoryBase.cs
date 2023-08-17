using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using TUTOR.Repository.EF;
using Microsoft.EntityFrameworkCore;
using TUTOR.Biz.Repository_Interfaces.Base;
using TUTOR.Biz.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace TUTOR.Repository.Repositories
{
    public abstract class EFRepositoryBase<TEntity, TDTO, TIdentity> : IRepository<TDTO, TIdentity>
        where TEntity : class
        where TDTO : IDTO
    {
        protected readonly IMapper _mapper;
        protected TUTORERPSContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EFRepositoryBase(IMapper mapper, TUTORERPSContext context)
        {
            _mapper = mapper;
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        protected void SetDbContext(TUTORERPSContext context)
        {
            _context = context;
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TDTO, bool>> predicate)
        {
            var expression = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(predicate);

            return _dbSet.AnyAsync(expression);
        }

        public virtual async Task DeleteAsync(TIdentity id)
        {
            var data = await _dbSet.FindAsync(id);
            if (data != null)
            {
                _dbSet.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Expression<Func<TDTO, bool>> predicate)
        {
            var expression = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(predicate);

            var entity = await _dbSet.SingleOrDefaultAsync(expression);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<TDTO> GetAsync(TIdentity id)
        {
            var entity = await _dbSet.FindAsync(id);

            return _mapper.Map<TDTO>(entity);
        }

        public virtual async Task<TDTO> GetAsync(Expression<Func<TDTO, bool>> predicate)
        {
            var expression = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(predicate);

            var entity = await _dbSet.SingleOrDefaultAsync(expression);

            return _mapper.Map<TDTO>(entity);
        }

        public virtual async Task<IEnumerable<TDTO>> GetListAsync(Expression<Func<TDTO, bool>> predicate)
        {
            var expression = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(predicate);

            var entities = await _dbSet.Where(expression).ToListAsync();

            return entities.Select(_mapper.Map<TDTO>);
        }

        public async Task<IEnumerable<TDTO>> GetListAsync()
        {
            var data = await _dbSet.ToListAsync();

            return data.Select(_mapper.Map<TDTO>);
        }

        public virtual async Task<TDTO> InsertAsync(TDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            dto = _mapper.Map<TDTO>(entity);

            return dto;
        }

        public virtual Task UpdateAsync(TDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<TDTO>> GetListAsync(Expression<Func<TDTO, bool>> predicate, string kind, string orderBy, int page, int? pageSize)
        {
            throw new NotImplementedException();
        }
    }
}