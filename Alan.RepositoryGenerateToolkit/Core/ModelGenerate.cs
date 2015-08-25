using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.RepositoryGenerateToolkit.Models;
using RazorEngine;

namespace Alan.RepositoryGenerateToolkit.Core
{
    public class ModelGenerate
    {
        public static string GetContext(IEnumerable<TableModel> tables)
        {
            var model = new { Tables = tables, Config = Configurations.Current };
            var template = File.ReadAllText(Configurations.Current.Model.ContextTemplatePath);
            var code = Razor.Parse(template, model);
            return code;
        }

        private static string GetModel(TableModel table)
        {
            var model = new { Table = table, Config = Configurations.Current };
            var template = File.ReadAllText(Configurations.Current.Model.ModelTemplatePath);
            var code = Razor.Parse(template, model);
            return code;
        }
        public static Dictionary<string, string> GetModels(IEnumerable<TableModel> tables)
        {
            var codes = new Dictionary<string, string>();
            tables.ToList().ForEach(table =>
            {
                codes.Add(table.TableName, GetModel(table));
            });
            return codes;
        }

        public static void SaveModels()
        {
            var codes = GetModels(DbAccess.Tables);
            codes.ToList().ForEach(code =>
            {
                File.WriteAllText(Path.Combine(Configurations.Current.Model.ModelSavePath, code.Key + ".cs"), code.Value);
            });
            var contextCode = GetContext(DbAccess.Tables);
            File.WriteAllText(
                Path.Combine(Configurations.Current.Model.ContextSavePath,
                    String.Format("{0}.cs", Configurations.Current.Model.ContextName)), contextCode);
        }
    }
}
