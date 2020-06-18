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
    public partial class ProductDetailService : BaseService<ProductDetails>, IProductDetailService
    {
        public ProductDetailService(IRepository<ProductDetails> repository) 
            : base(repository) { }
    }

    public partial class ProductDetailService : IProductDetailService
    {
        public async Task<ProductDetails> Insert(Guid createdBy, ProductDetails model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductVariantsId", SqlDbType.UniqueIdentifier, model.ProductVariantsId),
                    new ParamItem("ValueId", SqlDbType.Int, model.ValueId),
                    new ParamItem("Title", SqlDbType.NVarChar, model.Title ?? ""),
                    new ParamItem("SKU", SqlDbType.VarChar, model.SKU),
                    new ParamItem("BarCode", SqlDbType.VarChar, model.BarCode ?? ""),
                };
                return await Task.FromResult(base.SqlQuery("pro_ProductDetail_Insert", Params.Create(arr)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductDetailService at Insert() Method", ex.Message);
            }
            return null;
        }

        public async Task<long> Update(Guid createdBy, ProductDetails model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductDetailId", SqlDbType.Int, model.Id),
                    new ParamItem("Title", SqlDbType.NVarChar, model.Title ?? ""),
                    new ParamItem("SKU", SqlDbType.VarChar, model.SKU ?? ""),
                    new ParamItem("BarCode", SqlDbType.VarChar, model.BarCode ?? ""),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),

                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductDetail_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductDetailService at Update() Method", ex.Message);
            }
            return -1;
        }

        public async Task<ProductDetails> FindById(int id)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, id),
                };
                return await Task.FromResult(base.SqlQuery("pro_ProductDetail_FindById", Params.Create(arr)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductDetailService at FindById() Method", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<ProductDetails>> FindByProduct(Guid productId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                };
                return await Task.FromResult(base.SqlQuery("pro_ProductDetail_FindByProduct", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductDetailService at FindByProductId() Method", ex.Message);
            }
            return Enumerable.Empty<ProductDetails>();
        }
    }
}
