using GomiShop.Common.Configuration;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service.Products
{
    public partial interface IProductService : IBaseService<Product>
    {
        Task<long> Insert(Guid createdBy, Product model, int categoryId, int subCategoryId, List<int> collectionId);

        Task<long> Update(Guid createdBy, Product model, int categoryId, int subCategoryId, List<int> collectionId);

        Task<long> Delete(Guid createdBy, Guid id);

        Task<IEnumerable<Product>> FindBy(ProductFilter model);

        Task<Product> FindById(Guid id);

        Task<IEnumerable<Product>> FindByUserFavorite(Guid userId, ObjectType type);

        // Tags
        Task<long> InsertTags(Guid productId, List<string> tags);

        Task<long> DeleteAllTags(Guid productId);

        //...Tags./

        // Branch
        Task<long> InsertBranch(Guid createdBy, Guid productId, int branchId, DateTime displayDate, Status status);

        //...Branch./
    }
}