using GomiShop.Common.Parameter;
using GomiShop.Core.Interface.Data;
using GomiShop.Data.Interface;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace GomiShop.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabaseFactory databaseFactory;
        private readonly Database context;
        private IDataContext dataContext;

        protected IDatabaseFactory DatabaseFactory
        {
            get { return databaseFactory; }
        }

        protected Database Context
        {
            get { return context; }
        }

        protected IDataContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public Repository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            this.context = DataContext.GetDbContext();
        }

        public IEnumerable<T> SqlQuery(string procName)
        {
            try
            {
                return context.SqlQuery<T>(procName);
            }
            catch (SqlException ex)
            {
                this.WriteError("Error at SqlQuery() Method", ex.Message);
            }

            return Enumerable.Empty<T>();
        }

        public IEnumerable<T> SqlQuery(string procName, SqlParameter[] sqlParams)
        {
            try
            {
                return context.SqlQuery<T>(CreateSqlCommand(procName, sqlParams), sqlParams);
            }
            catch (SqlException ex)
            {
                this.WriteError("Error at SqlQuery() Method with parameter", ex.Message);
            }

            return Enumerable.Empty<T>();
        }

        public long ExecuteSql(string procName, SqlParameter[] sqlParams)
        {
            try
            {
                return context.ExecuteSqlCommand(CreateSqlCommand(procName, sqlParams), sqlParams);
            }
            catch (SqlException ex)
            {
                this.WriteError("Error At ExecuteSql() Method", ex.Message);
            }

            return -1;
        }

        public void WriteError(string title, string content)
        {
            ParamItem[] arr = new ParamItem[] { new ParamItem("Title", SqlDbType.VarChar, title),
                                                new ParamItem("Content", SqlDbType.VarChar, content)};

            this.ExecuteSql("pro_ErrorLog_Insert", Params.Create(arr));
        }

        private string CreateSqlCommand(string _StoredProcName, SqlParameter[] _SqlParams)
        {
            string sql = _StoredProcName;

            for (int i = 0; i < _SqlParams.Count(); i++)
                sql += (i < _SqlParams.Count() - 1) ? " @" + _SqlParams[i].ParameterName + "," : " @" + _SqlParams[i].ParameterName;

            return sql;
        }
    }
}