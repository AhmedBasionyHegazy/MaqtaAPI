using AutoMapper;
using Maqta.API.DBContexts;
using Maqta.API.Repository;
using Maqta.API.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDBContext _context;
        private IDbContextTransaction _transaction;
        private IMapper _mapper;
        public IProductRepository Products { get; private set; }

        public UnitOfWork(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Products = new ProductRepository(_context, _mapper);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void StartTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_context == null)
            {
                return;
            }

            _context.Dispose();
            _context = null;
        }
    }
}
