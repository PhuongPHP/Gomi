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
    public partial class ProductAttributeService : BaseService<ProductAttribute>, IProductAttributeService
    {
        public ProductAttributeService(IRepository<ProductAttribute> repository) 
            : base(repository) { }
    }

    public partial class ProductAttributeService : IProductAttributeService
    {
        public async Task<long> Insert(Guid createdBy, ProductAttribute model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, model.ProductId),
                    new ParamItem("AttributeId", SqlDbType.Int, model.AttributeId),
                    new ParamItem("Content_vi", SqlDbType.NVarChar, model.Content_vi),
                    new ParamItem("Content_en", SqlDbType.VarChar, model.Content_en),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductAttribute_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductAttributeService at Insert() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> DeleteAll(Guid createdBy, Guid productId)
        {

            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductAttribute_DeleteAll", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductAttributeService at DeleteAll() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> InsertVariant(Guid createdBy, Guid id, Guid productId, int variantValueId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, id),
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                    new ParamItem("VariantValueId", SqlDbType.Int, variantValueId),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductVariant_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at InsertVariant() Method", ex.Message);
            }
            return -1;
        }


        public async Task<IEnumerable<ProductAttribute>> FindByProduct(Guid productId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                };
                return await Task.FromResult(base.SqlQuery("pro_ProductAttribute_FindByProduct", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at FindByProduct() Method", ex.Message);
            }
            return Enumerable.Empty<ProductAttribute>();
        }
    }
}
