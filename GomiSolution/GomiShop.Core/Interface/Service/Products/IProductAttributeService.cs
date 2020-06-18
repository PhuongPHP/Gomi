using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service.Products
{
    public partial interface IProductAttributeService : IBaseService<ProductAttribute>
    {
        Task<long> Insert(Guid createBy, ProductAttribute model);
        Task<long> DeleteAll(Guid createdBy, Guid productId);
        Task<IEnumerable<ProductAttribute>> FindByProduct(Guid productId);
    }
}
