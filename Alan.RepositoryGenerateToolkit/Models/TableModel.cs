using System;
using System.Collections.Generic;
using System.Linq;
using Alan.RepositoryGenerateToolkit.Utils;
using System.Data;
using System.Data.Linq;

namespace Alan.RepositoryGenerateToolkit.Models
{
    public class TableModel
    {
        private string _tableName;

        public string TableName
        {
            get { return this._tableName.UpperFirstLetter(); }
            set
            {
                this._tableName = value;
            }
        }

        public List<ColumnModel> Columns { get; set; }
        public List<string> PrimaryKeyNames { get; set; }
        public List<string> IdentityColumnNames { get; set; }
        public TableModel()
        {
            this.TableName = String.Empty;
            this.Columns = new List<ColumnModel>();
            this.PrimaryKeyNames = new List<string>();
            this.IdentityColumnNames = new List<string>();
        }
        public void SetIdentityColumnNames(DataContext context)
        {
            this.IdentityColumnNames = context.ExecuteQuery<string>("select Name from sys.identity_columns where object_name(object_id) = {0}", this.TableName).ToList();
        }
        public void SetPrimaryKeyNames(DataContext context)
        {
            this.PrimaryKeyNames = context.ExecuteQuery<string>("SELECT column_name as ColumnName FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = {0}", this.TableName).ToList();
        }
    }
}
