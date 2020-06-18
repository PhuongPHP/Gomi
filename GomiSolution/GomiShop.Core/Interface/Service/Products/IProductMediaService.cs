using GomiShop.Common.Configuration;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service.Products
{
    public partial interface IProductMediaService : IBaseService<ProductMedia>
    {
        Task<long> Insert(Guid createdBy, ProductMedia model);

        Task<long> Update(Guid id, int index, Status status);

        Task<IEnumerable<ProductMedia>> FindByProduct(Guid productId);

        Task<IEnumerable<ProductMedia>> FindByProductVariant(Guid productVariantId);
    }
}