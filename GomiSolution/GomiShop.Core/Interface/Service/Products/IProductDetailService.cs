using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service.Products
{
    public partial interface IProductDetailService : IBaseService<ProductDetails>
    {
        Task<ProductDetails> Insert(Guid createdBy, ProductDetails model);

        Task<long> Update(Guid createdBy, ProductDetails model);

        Task<ProductDetails> FindById(int id);

        Task<IEnumerable<ProductDetails>> FindByProduct(Guid productId);
    }
}
