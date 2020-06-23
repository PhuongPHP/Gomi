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
    public partial class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository) : base(repository) { }
    }

    public partial class CategoryService : ICategoryService
    {
        public async Task<long> Insert(Guid createdBy, Category model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Name_vi", SqlDbType.NVarChar, model.Name_vi ?? ""),
                    new ParamItem("Name_en", SqlDbType.VarChar, model.Name_en ?? ""),
                    new ParamItem("MegaId", SqlDbType.Int, model.MegaId),
                    new ParamItem("Position", SqlDbType.TinyInt, model.Position),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),

                };

                return await Task.FromResult(base.ExecuteSql("pro_Category_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in CategoryService at Insert() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Update(Guid createdBy, Category model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, model.Id),
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Name_vi", SqlDbType.NVarChar, model.Name_vi ?? ""),
                    new ParamItem("Name_en", SqlDbType.VarChar, model.Name_en ?? ""),
                    new ParamItem("MegaId", SqlDbType.Int, model.MegaId),
                    new ParamItem("Position", SqlDbType.TinyInt, model.Position),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),
                };

                return await Task.FromResult(base.ExecuteSql("pro_Category_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in CategoryService at Update() Method", ex.Message);
            }
            return -1;
        }

        public async Task<Category> FindById(int id)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, id)
                };
                return await Task.FromResult(base.SqlQuery("pro_Category_FindById", Params.Create(arr)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in CategoryService at FindById() Method", ex.Message);
            }

            return null;
        }

        public async Task<IEnumerable<Category>> FindBy(string keyword, int megaId, Status status, int beginRow, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Keyword", SqlDbType.NVarChar, keyword.KeywordContains()),
                    new ParamItem("MegaId", SqlDbType.Int, megaId),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                    new ParamItem("NumRows", SqlDbType.Int, numRows),
                };
                return await Task.FromResult(base.SqlQuery("pro_Category_FindBy", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in CategoryService at FindBy() Method", ex.Message);
            }

            return Enumerable.Empty<Category>();
        }
    }
}
