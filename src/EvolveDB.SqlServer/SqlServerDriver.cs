using EvolveDB.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EvolveDB.SqlServer
{
    public class SqlServerDriver : IDriver
    {
        #region Fields
        private readonly SqlConnection _connection;
        #endregion

        #region Constructors
        public SqlServerDriver(EvolveBuilderContext context)
        {
            _connection = null;
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        public void Dispose()
        {
            _connection.Dispose();
        }
        #endregion
    }
}
