using GomiShop.Common.Configuration;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service
{
    public interface ISubCategoryService : IBaseService<SubCategory>
    {
        Task<long> Insert(Guid createdBy, SubCategory model);

        Task<long> Update(Guid createdBy, SubCategory model);

        Task<SubCategory> FindById(int id);

        Task<IEnumerable<SubCategory>> FindBy(string keyword, int cateId, Status status, int beginRow, int numRows);
    }
}