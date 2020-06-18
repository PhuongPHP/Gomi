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
    public partial class PriceListService : BaseService<PriceList>, IPriceListService
    {
        public PriceListService(IRepository<PriceList> repository)
            : base(repository) { }
    }

    public partial class PriceListService : IPriceListService
    {
        public async Task<long>Insert(Guid createdBy, PriceList model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[]
                {
                    new ParamItem("CreatedBy", SqlDbType.UniqueIdentifier, createdBy),
                    new ParamItem("ProductDetailId", SqlDbType.Int, model.ProductDetailId),
                    new ParamItem("SalePrice", SqlDbType.Decimal, model.SalePrice),
                    new ParamItem("MarketPrice", SqlDbType.Decimal, model.MarketPrice),
                    new ParamItem("SellerPercent", SqlDbType.Int, model.SellerPercent),
                };
                return await Task.FromResult(base.ExecuteSql("pro_PriceList_Insert", Params.Create(arr)));
            }
            catch(Exception ex)
            {
                base.WriteError("Error in PriceListService at Insert() Method", ex.Message);
            }
            return -1;
        }
        
    }
}
