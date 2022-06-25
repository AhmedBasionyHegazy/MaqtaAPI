using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Maqta.API.Repository
{
    public interface IRepository<TContext, T, TDto> where T : class where TContext : DbContext where TDto : class
    {
        Task<TDto> GetFirstOrDefault(Expression<Func<T, bool>> filter, bool? disableTracking = false, string? includeProperties = null);
        Task<IEnumerable<TDto>> GetAll(string? includeProperties = null);
        Task<IEnumerable<TDto>> GetAllRecords(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        TDto Add(TDto entityDto);
        bool Remove(TDto entityDto);
        bool RemoveRange(IEnumerable<TDto> entityDtos);
        TDto Update(TDto entityDto);
        Task<int> SaveChanges();
    }
}
