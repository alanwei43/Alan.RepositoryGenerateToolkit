using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.RepositoryGenerateToolkit.Utils;

namespace Alan.RepositoryGenerateToolkit.Models
{
    /// <summary>
    /// Table column model
    /// </summary>
    public class ColumnModel
    {
        private string _name;

        public string Name
        {
            get { return (this._name ?? "").UpperFirstLetter(); }
            set { this._name = value; }
        }

        public bool IsNullable { get; set; }

        public string DataType
        {
            get { return ConvertType(); }
            set { }
        }

        public string DbType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsDbGenerated { get; set; }
        public int? Length { get; set; }

        public string AutoSync
        {
            get { return this.IsDbGenerated ? "AutoSync.OnInsert" : "AutoSync.Never"; }
            set { }
        }

        public string RichDbType
        {
            get
            {
                var dataBaseType = this.DbType;
                if (this.Length != null)
                {
                    var length = this.Length.GetValueOrDefault() == -1 ? "MAX" : this.Length.ToString();
                    dataBaseType = String.Format("{0}({1})", dataBaseType, length);
                }
                var nullable = this.IsNullable ? "" : "NOT NULL";
                var identity = this.IsDbGenerated ? "IDENTITY" : "";

                var richType = String.Format("{0} {1} {2}", dataBaseType, nullable, identity).Trim();
                return richType;
            }
            set { }
        }


        private string ConvertType()
        {
            //Reference: https://msdn.microsoft.com/en-us/library/vstudio/bb386947(v=vs.100).aspx
            var dbType = this.DbType.ToLower();
            
            string clrType;
            switch (dbType)
            {
                case "varchar":
                case "nvarchar":
                case "ntext":
                case "text":
                    clrType = "string";
                    break;
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                case "date":
                case "time":
                    clrType = this.IsNullable ? "DateTime?" : "DateTime";
                    break;
                case "datetimeoffset":
                    clrType = this.IsNullable ? "DateTimeOffset?" : "DateTimeOffset";
                    break;

                case "bit":
                    clrType = this.IsNullable ? "bool?" : "bool";
                    break;
                case "tinyint":
                    clrType = this.IsNullable ? "byte?" : "byte";
                    break;
                case "smallint":
                    clrType = this.IsNullable ? "short?" : "short";
                    break;
                case "int":
                    clrType = this.IsNullable ? "int?" : "int";
                    break;
                case "bigint":
                    clrType = this.IsNullable ? "long?" : "long";
                    break;

                case "uniqueidentifier":
                    clrType = this.IsNullable ? "Guid?" : "Guid";
                    break;

                case "decimal":
                case "smallmoney":
                case "money":
                    clrType = this.IsNullable ? "decimal?" : "decimal";
                    break;

                case "double":
                    clrType = this.IsNullable ? "double?" : "double";
                    break;
                case "float":
                    clrType = this.IsNullable ? "float?" : "float";
                    break;
                default:
                    throw new Exception("找不到匹配的数据类型.");
            }


            return clrType;
        }
    }


}
