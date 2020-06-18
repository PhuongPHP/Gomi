using GomiShop.Common.Disposable;
using GomiShop.Data.Interface;

namespace GomiShop.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private IDataContext dataContext;

        public IDataContext Get()
        {
            return dataContext ?? (dataContext = new DatabaseContext());
        }

        protected override void DisposeCore()
        {
            base.DisposeCore();
        }
    }
}