using AutoMapper;
using Maqta.API.DBContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Maqta.API.Repository
{
    public class Repository<TContext, T, TDto> : IRepository<TContext, T, TDto> where T : class where TContext : DbContext where TDto : class
    {
        private TContext _db;
        private IMapper _mapper;
        internal DbSet<T> dbSet;
        public Repository(TContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            this.dbSet = _db.Set<T>();
        }
        public TDto Add(TDto entityDto)
        {
            T entity = _mapper.Map<TDto, T>(entityDto);
            dbSet.Add(entity);
            return _mapper.Map<TDto>(entity);
        }
        public async Task<IEnumerable<TDto>> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return _mapper.Map<IEnumerable<TDto>>(await query.ToListAsync());
        }
        public async Task<TDto> GetFirstOrDefault(Expression<Func<T, bool>> filter,bool? disableTracking=false, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = disableTracking.Value? query.Where(filter).AsNoTracking(): query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return _mapper.Map<TDto>(await query.FirstOrDefaultAsync());
        }
        public bool Remove(TDto entityDto)
        {
            try
            {
                if (entityDto == null)
                {
                    return false;
                }
                T entity = _mapper.Map<T>(entityDto);
                dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool RemoveRange(IEnumerable<TDto> entityDtos)
        {
            try
            {
                IEnumerable<T> entities = _mapper.Map<IEnumerable<T>>(entityDtos);
                dbSet.RemoveRange(entities);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public TDto Update(TDto entityDto)
        {
            T entity = _mapper.Map<TDto, T>(entityDto);
            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbSet.Update(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<IEnumerable<TDto>> GetAllRecords(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) { query = query.Where(filter); }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return _mapper.Map<IEnumerable<TDto>>(await query.ToListAsync());
        }

        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
