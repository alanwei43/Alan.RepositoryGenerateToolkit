using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.RepositoryGenerateToolkit.Models;
using RazorEngine;

namespace Alan.RepositoryGenerateToolkit.Core
{
    public class RepositoryGenerate
    {

        public static string GetGenericIRepository()
        {
            var config = Configurations.Current;
            var template = File.ReadAllText(config.Repository.GenericIRepositoryTemplatePath);
            var code = Razor.Parse(template, config);
            return code;
        }
        public static string GetGenericRepository()
        {
            var config = Configurations.Current;
            var template = File.ReadAllText(config.Repository.GenericRepositoryTemplatePath);
            var code = Razor.Parse(template, config);
            return code;
        }

        private static string GetReposity(TableModel table)
        {
            var model = new { Table = table, Config = Configurations.Current };
            var template = File.ReadAllText(Configurations.Current.Repository.RepositoryTemplatePath);
            var code = Razor.Parse(template, model);
            return code;
        }
        public static Dictionary<string, string> GetRepositories(IEnumerable<TableModel> tableModels)
        {
            var repositories = new Dictionary<string, string>();
            tableModels.ToList().ForEach(table =>
            {
                repositories.Add(table.TableName, GetReposity(table));
            });
            return repositories;
        }


        private static string GetIReposity(TableModel table)
        {
            var model = new { Table = table, Config = Configurations.Current };
            var template = File.ReadAllText(Configurations.Current.Repository.IRepositoryTemplatePath);
            var code = Razor.Parse(template, model);
            return code;
        }
        public static Dictionary<string, string> GetIRepositories(IEnumerable<TableModel> tableModels)
        {
            var repositories = new Dictionary<string, string>();
            tableModels.ToList().ForEach(table =>
            {
                repositories.Add(table.TableName, GetIReposity(table));
            });
            return repositories;
        }

        public static void SaveRepositories()
        {
            var tables = DbAccess.Tables;
            tables.ToList().ForEach(table =>
            {
                var repositoryCode = GetReposity(table);
                File.WriteAllText(
                    Path.Combine(Configurations.Current.Repository.RepositorySavePath, table.TableName + ".cs"), repositoryCode);

                var irepositoryCode = GetIReposity(table);
                File.WriteAllText(Path.Combine(Configurations.Current.Repository.IRepositorySavePath, String.Format("I{0}.cs", table.TableName)), irepositoryCode);
            });
            var genericRepCode = GetGenericRepository();
            File.WriteAllText(Path.Combine(Configurations.Current.Repository.RepositorySavePath, String.Format("{0}.cs", Configurations.Current.Repository.GenericRepositoryName)), genericRepCode);
            var genericIRepCod = GetGenericIRepository();
            File.WriteAllText(Path.Combine(Configurations.Current.Repository.IRepositorySavePath, String.Format("{0}.cs", Configurations.Current.Repository.GenericIRepositoryName)), genericIRepCod);
        }
    }
}
