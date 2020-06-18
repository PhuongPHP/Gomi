using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.Interface.Service.Products
{
    public partial interface IPriceListService : IBaseService<PriceList>
    {
        Task<long> Insert(Guid createdBy, PriceList model);
    }
}
