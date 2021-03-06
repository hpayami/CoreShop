using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetProducsAsync() =>
            await _context.Products
            .Include(i=> i.ProductBrand)
            .Include(i => i.ProductType)
            .ToListAsync();

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync() =>
            await _context.productBrands.ToListAsync();
        

        public async Task<Product> GetProductByIdAsync(int id) =>
            await _context.Products
            .Include(i=> i.ProductBrand)
            .Include(i => i.ProductType)
            .FirstOrDefaultAsync( p=> p.Id == id);

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() =>
            await _context.productTypes.ToListAsync();
    }
}