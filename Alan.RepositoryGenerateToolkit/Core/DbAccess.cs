using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.RepositoryGenerateToolkit.Models;

namespace Alan.RepositoryGenerateToolkit.Core
{
    public class DbAccess
    {

        public static IEnumerable<TableModel> Tables { get; set; }

        static DbAccess()
        {
            var access = new DbAccess();
            Tables = access.GetTables();
        }




        public IEnumerable<TableModel> GetTables()
        {
            var cxt = new ModelContext();
            var tables =
                cxt.ExecuteQuery<TableModel>("select Table_Name as TableName from INFORMATION_SCHEMA.TABLES").ToList();

            tables.ForEach(t =>
            {
                t.SetPrimaryKeyNames(cxt);
                t.SetIdentityColumnNames(cxt);
                t.Columns = cxt.ExecuteQuery<ColumnModel>(
                    String.Format(
                        "select COLUMN_NAME as Name, DATA_TYPE as DbType, cast(case when IS_NULLABLE = 'YES' then 1 else 0 end as bit) as IsNullable, CHARACTER_MAXIMUM_LENGTH as [Length] from INFORMATION_SCHEMA.COLUMNS where Table_Name='{0}'", 
                        t.TableName)).ToList();

                t.Columns.ForEach(c =>
                {
                    c.IsPrimaryKey = t.PrimaryKeyNames.Contains(c.Name);
                    c.IsDbGenerated = t.IdentityColumnNames.Contains(c.Name);
                });
            });

            return tables;
        }

    }
}
