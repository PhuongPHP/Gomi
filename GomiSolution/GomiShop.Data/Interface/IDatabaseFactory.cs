using System;

namespace GomiShop.Data.Interface
{
    public interface IDatabaseFactory : IDisposable
    {
        IDataContext Get();
    }
}
