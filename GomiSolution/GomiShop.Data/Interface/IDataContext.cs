using System;
using System.Data.Entity;

namespace GomiShop.Data.Interface
{
    public interface IDataContext : IDisposable
    {
        Database GetDbContext();
    }
}
