using GomiShop.Common.Configuration;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service
{
    public partial interface IMegaCategoryService : IBaseService<MegaCategory>
    {
        Task<long> Insert(Guid createdBy, MegaCategory model);
        Task<long> Update(Guid createdBy, MegaCategory model);
        Task<MegaCategory> FindById(int id);
        Task<IEnumerable<MegaCategory>> FindBy(string keyword, Status status, int beginRow, int numRows);

    }
}
