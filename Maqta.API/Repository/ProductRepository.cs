using AutoMapper;
using Maqta.API.DBContexts;
using Maqta.API.Models;
using Maqta.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Repository
{
    public class ProductRepository :Repository<ApplicationDBContext,Product, ProductDto>, IProductRepository
    {
        private readonly ApplicationDBContext _context;
        private IMapper _mapper;
        public ProductRepository(ApplicationDBContext context, IMapper mapper):base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
