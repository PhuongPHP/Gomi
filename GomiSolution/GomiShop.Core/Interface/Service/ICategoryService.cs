using GomiShop.Common.Configuration;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service
{
    public partial interface ICategoryService : IBaseService<Category>
    {
        Task<long> Insert(Guid createdBy, Category model);
        Task<long> Update(Guid createdBy, Category model);

        Task<Category> FindById(int id);
        Task<IEnumerable<Category>> FindBy(string keyword, int megaId, Status status, int beginRow, int numRows);

    }
}
