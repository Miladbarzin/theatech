using DataLayer.DBModels;
using DataLayer.Enum;
using DataLayer.Repository;
using DataLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class ProductServices: IProductServices
    {
        SQLServerContext _db;
        public ProductServices(SQLServerContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var list =  await _db.Products.Select(p => new ProductViewModel { 
                ID = p.ID,
                ProductName = p.ProductName,
                Color= p.Color, 
                Description= p.Description, 
                Price= p.Price, 
                Size = p.Size   
            }).ToListAsync();

            return list;
        }

        public async Task<DBModels.Product?> AddProductAsync(ProductViewModel _productViewModel)
        {
            try
            {
                var product = await _db.Products.AddAsync(new Product
                {
                    ProductName = _productViewModel.ProductName,
                    Color = _productViewModel.Color,
                    Description = _productViewModel.Description,
                    Price = _productViewModel.Price,
                    Size = _productViewModel.Size,
                });
                await _db.SaveChangesAsync();

                return product.Entity;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProductEnum> DeleteProductAsync(int productID)
        {
            try
            {
                // IF product exist in cart we can not delete
                if (_db.Carts.Any(p => p.ProductID == productID))
                    return ProductEnum.NotAllow;

                var product = _db.Products.FirstOrDefault(p => p.ID == productID);
                if (product == null) return ProductEnum.NotExist;

                product.isActive = false;
                _db.Products.Update(product);
                _db.SaveChanges();

                return ProductEnum.Ok;
            }
            catch
            {
                return ProductEnum.InternalServerError;

            }
        }
    }
}
