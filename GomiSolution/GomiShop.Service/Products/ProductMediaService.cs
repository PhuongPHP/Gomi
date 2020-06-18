using GomiShop.Common.Configuration;
using GomiShop.Common.Parameter;
using GomiShop.Core.Interface.Data;
using GomiShop.Core.Interface.Service.Products;
using GomiShop.Core.Model;
using GomiShop.Service.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Service.Products
{
    public partial class ProductMediaService : BaseService<ProductMedia>, IProductMediaService
    {
        public ProductMediaService(IRepository<ProductMedia> repository) 
            : base(repository) { }
    }

    public partial class ProductMediaService : IProductMediaService
    {
        public async Task<long> Insert(Guid createdBy, ProductMedia model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, model.Id),
                    new ParamItem("ProductVariantId", SqlDbType.UniqueIdentifier, model.ProductVariantId),
                    new ParamItem("MediaPath", SqlDbType.VarChar, model.MediaPath ?? ""),
                    new ParamItem("Thumbnail", SqlDbType.VarChar, model.Thumbnail?? ""),
                    new ParamItem("Index", SqlDbType.Int, model.Index),
                    new ParamItem("Type", SqlDbType.TinyInt, (int)model.Type),
                };
                return await Task.FromResult(base.ExecuteSql("pro_PriceMedia_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductMediaService at Insert() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Update(Guid id, int index, Status status)
        {

            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, id),
                    new ParamItem("Index", SqlDbType.Int, index),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductMedia_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductMediaService at Update() Method", ex.Message);
            }
            return -1;
        }


        public async Task<IEnumerable<ProductMedia>> FindByProduct(Guid productId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                };
                return await Task.FromResult(base.SqlQuery("pro_ProductMedia_FindByProduct", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductMediaService at FindByProductId() Method", ex.Message);
            }
            return Enumerable.Empty<ProductMedia>();
        }

        public async Task<IEnumerable<ProductMedia>> FindByProductVariant(Guid productVariantId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("ProductVariantId", SqlDbType.UniqueIdentifier, productVariantId),
                };
                return await Task.FromResult(base.SqlQuery("pro_ProductMedia_FindByProductVariant", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductMediaService at FindByProductVariant() Method", ex.Message);
            }
            return Enumerable.Empty<ProductMedia>();
        }
    }
}
