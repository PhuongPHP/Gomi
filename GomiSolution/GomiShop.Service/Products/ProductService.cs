using GomiShop.Common.Configuration;
using GomiShop.Common.Extensions;
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
    public partial class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> repository) 
            : base(repository) { }

    }

    public partial class ProductService : IProductService
    {
        public async Task<long> Insert(Guid createdBy, Product model, int categoryId, int subCategoryId, List<int> collectionId)
        {
            try
            {
                DataTable dtCollection = new DataTable();
                dtCollection.Columns.AddRange(new DataColumn[1] { new DataColumn("id", typeof(int)), });
                foreach (var item in collectionId)
                {
                    dtCollection.Rows.Add(item);
                }

                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, model.Id),
                    new ParamItem("Name", SqlDbType.NVarChar, model.Name ?? ""),
                    new ParamItem("Summary", SqlDbType.NVarChar, model.Summary ?? ""),
                    new ParamItem("Description", SqlDbType.NText, model.Description ?? ""),
                    new ParamItem("BrandId", SqlDbType.Int, model.BrandId),
                    new ParamItem("CategoryId", SqlDbType.Int, categoryId),
                    new ParamItem("SubCategoryId", SqlDbType.Int, subCategoryId),
                    new ParamItem("CollectionId", SqlDbType.Structured, dtCollection, "list_id_table"),
                };
                return await Task.FromResult(base.ExecuteSql("pro_Product_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at Insert() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Update(Guid createdBy, Product model, int categoryId, int subCategoryId, List<int> collectionId)
        {
            try
            {
                DataTable dtCollection = new DataTable();
                dtCollection.Columns.AddRange(new DataColumn[1] { new DataColumn("id", typeof(int)), });
                foreach (var item in collectionId)
                {
                    dtCollection.Rows.Add(item);
                }

                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, model.Id),
                    new ParamItem("Name", SqlDbType.NVarChar, model.Name ?? ""),
                    new ParamItem("Summary", SqlDbType.NVarChar, model.Summary ?? ""),
                    new ParamItem("Description", SqlDbType.NText, model.Description ?? ""),
                    new ParamItem("BrandId", SqlDbType.Int, model.BrandId),
                    new ParamItem("CategoryId", SqlDbType.Int, categoryId),
                    new ParamItem("SubCategoryId", SqlDbType.Int, subCategoryId),
                    new ParamItem("CollectionId", SqlDbType.Structured, dtCollection, "list_id_table"),
                };
                return await Task.FromResult(base.ExecuteSql("pro_Product_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at Update() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Delete(Guid createdBy, Guid id)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, id),
                };
                return await Task.FromResult(base.ExecuteSql("pro_Product_Delete", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at Delete() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> InsertTags(Guid productId, List<string> tags)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[1] { new DataColumn("value", typeof(string)) });
                foreach (string value in tags)
                {
                    if (!String.IsNullOrEmpty(value.Trim()))
                        dt.Rows.Add(value.Trim());
                }

                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                    new ParamItem("ListTags", SqlDbType.Structured, dt, "list_strId_table"),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductTags_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at InsertTags() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> DeleteAllTags(Guid productId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductTags_DeleteByProduct", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at DeleteAllTags() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> InsertBranch(Guid createdBy, Guid productId, int branchId, DateTime displayDate, Status status)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                    new ParamItem("BranchId", SqlDbType.Int, branchId),
                    new ParamItem("DisplayDate", SqlDbType.DateTime, displayDate),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductBranch_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at InsertBranch() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> InsertVariant(Guid createdBy, Guid id, Guid productId, int variantValueId, int index)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, id),
                    new ParamItem("ProductId", SqlDbType.UniqueIdentifier, productId),
                    new ParamItem("VariantValueId", SqlDbType.Int, variantValueId),
                    new ParamItem("Index", SqlDbType.Int, index),
                };
                return await Task.FromResult(base.ExecuteSql("pro_ProductVariant_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at InsertVariant() Method", ex.Message);
            }
            return -1;
        }

        public async Task<IEnumerable<Product>> FindBy(ProductFilter model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Keyword", SqlDbType.NVarChar, model.Keyword.KeywordContains()),
                    new ParamItem("ShopId", SqlDbType.UniqueIdentifier, model.ShopId == Guid.Empty ? DBNull.Value : (object)model.ShopId),
                    new ParamItem("MegaCateId", SqlDbType.Int, model.MegaCateId),
                    new ParamItem("BrandId", SqlDbType.Int, model.BrandId),
                    new ParamItem("MediaType", SqlDbType.TinyInt, (int)model.MediaType),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),
                    new ParamItem("BeginRow", SqlDbType.Int, model.BeginRow),
                    new ParamItem("NumRows", SqlDbType.Int, model.NumRows),
                };
                return await Task.FromResult(base.SqlQuery("pro_Product_FindBy", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at FindBy() Method", ex.Message);
            }
            return Enumerable.Empty<Product>();
        }


        public async Task<Product> FindById(Guid id)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.UniqueIdentifier, id),
                };
                return await Task.FromResult(base.SqlQuery("pro_Product_FindById", Params.Create(arr)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at FindById() Method", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Product>> FindByUserFavorite(Guid userId, ObjectType type)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, userId),
                    new ParamItem("Type", SqlDbType.TinyInt, type),
                };

                return await Task.FromResult(base.SqlQuery("pro_Product_FindByUserFavorite", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at FindByUserFavorite() Method", ex.Message);
            }
            return Enumerable.Empty<Product>();
        }
    }
}
