using GomiShop.Common.Configuration;
using GomiShop.Common.Extensions;
using GomiShop.Common.Parameter;
using GomiShop.Core.Interface.Data;
using GomiShop.Core.Interface.Service;
using GomiShop.Core.Model;
using GomiShop.Service.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Service
{
    public partial class SubCategoryService : BaseService<SubCategory>, ISubCategoryService
    {
        public SubCategoryService(IRepository<SubCategory> repository) : base(repository) { }
    }

    public partial class SubCategoryService : ISubCategoryService
    {
        public async Task<long> Insert(Guid createdBy, SubCategory model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Name_vi", SqlDbType.NVarChar, model.Name_vi ?? ""),
                    new ParamItem("Name_en", SqlDbType.VarChar, model.Name_en ?? ""),
                    new ParamItem("CategoryId", SqlDbType.Int, model.CategoryId),
                    new ParamItem("Position", SqlDbType.TinyInt, model.Position),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),

                };

                return await Task.FromResult(base.ExecuteSql("pro_Subcategory_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in SubCategoryService at Insert() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Update(Guid createdBy, SubCategory model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, model.Id),
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Name_vi", SqlDbType.NVarChar, model.Name_vi ?? ""),
                    new ParamItem("Name_en", SqlDbType.VarChar, model.Name_en ?? ""),
                    new ParamItem("CategoryId", SqlDbType.Int, model.CategoryId),
                    new ParamItem("Position", SqlDbType.TinyInt, model.Position),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),
                };

                return await Task.FromResult(base.ExecuteSql("pro_Subcategory_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in SubCategoryService at Update() Method", ex.Message);
            }
            return -1;
        }

        public async Task<SubCategory> FindById(int id)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, id)
                };
                return await Task.FromResult(base.SqlQuery("pro_SubCategory_FindById", Params.Create(arr)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in SubCategoryService at FindById() Method", ex.Message);
            }

            return null;
        }

        public async Task<IEnumerable<SubCategory>> FindBy(string keyword, int cateId, Status status, int beginRow, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Keyword", SqlDbType.NVarChar, keyword.KeywordContains()),
                    new ParamItem("CateId", SqlDbType.Int, cateId),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                    new ParamItem("NumRows", SqlDbType.Int, numRows),
                };
                return await Task.FromResult(base.SqlQuery("pro_SubCategory_FindBy", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in CategoryService at FindBy() Method", ex.Message);
            }

            return Enumerable.Empty<SubCategory>();
        }

    }
}
