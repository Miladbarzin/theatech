using DataLayer.DBModels;
using DataLayer.Enum;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<DBModels.Product?> AddProductAsync(ProductViewModel product);
        Task<ProductEnum> DeleteProductAsync(int productID);
    }
}
