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
    public partial class MegaCategoryService : BaseService<MegaCategory>, IMegaCategoryService
    {
        public MegaCategoryService(IRepository<MegaCategory> repository) : base(repository) { }
    }

    public partial class MegaCategoryService : IMegaCategoryService
    {
        public async Task<long> Insert(Guid createdBy, MegaCategory model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Name_vi", SqlDbType.NVarChar, model.Name_vi ?? ""),
                    new ParamItem("Name_en", SqlDbType.VarChar, model.Name_en ?? ""),
                    new ParamItem("Avatar", SqlDbType.VarChar, model.Avatar),
                    new ParamItem("Cover", SqlDbType.VarChar, model.Cover),
                    new ParamItem("Position", SqlDbType.TinyInt, (int)model.Position),
                    new ParamItem("Type", SqlDbType.TinyInt, (int)model.Type),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),

                };

                return await Task.FromResult(base.ExecuteSql("pro_MegaCategory_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MegaCategoryService at Insert() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Update(Guid createdBy, MegaCategory model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, model.Id),
                    new ParamItem("UserId", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("Name_vi", SqlDbType.NVarChar, model.Name_vi ?? ""),
                    new ParamItem("Name_en", SqlDbType.VarChar, model.Name_en ?? ""),
                    new ParamItem("Avatar", SqlDbType.VarChar, model.Avatar ?? ""),
                    new ParamItem("Cover", SqlDbType.VarChar, model.Cover ?? ""),
                    new ParamItem("Position", SqlDbType.TinyInt, (int)model.Position),
                    new ParamItem("Type", SqlDbType.TinyInt, (int)model.Type),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)model.Status),
                };

                return await Task.FromResult(base.ExecuteSql("pro_MegaCategory_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MegaCategoryService at Update() Method", ex.Message);
            }
            return -1;
        }

        public async Task<MegaCategory> FindById(int id)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Id", SqlDbType.Int, id),
                };
                return await Task.FromResult(base.SqlQuery("pro_MegaCategory_FindById", Params.Create(arr)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MegaCategoryService at FindById() Method", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<MegaCategory>> FindBy(string keyword, Status status, int beginRow, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("Keyword", SqlDbType.NVarChar, keyword.KeywordContains()),
                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                    new ParamItem("NumRows", SqlDbType.Int, numRows),

                };
                return await Task.FromResult(base.SqlQuery("pro_MegaCategory_FindBy", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MegaCategoryService at FindBy() Method", ex.Message);
            }

            return Enumerable.Empty<MegaCategory>();
        }
    }
}
