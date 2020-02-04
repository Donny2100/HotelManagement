using System;
using System.Data;
using System.Data.Common;
using MC.ORM.Core;
using MC.ORM.Utilities;

namespace MC.ORM.Providers
{
    public class MsAccessDbDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return DbProviderFactories.GetFactory("System.Data.OleDb");
        }

        public override object ExecuteInsert(Database database, IDbCommand cmd, string primaryKeyName)
        {
            database.ExecuteNonQueryHelper(cmd);
            cmd.CommandText = "SELECT @@IDENTITY AS NewID;";
            return database.ExecuteScalarHelper(cmd);
        }

        public override string BuildPageQuery(long skip, long take, SQLParts parts, ref object[] args)
        {
            throw new NotSupportedException("The Access provider does not support paging.");
        }
    }
}
