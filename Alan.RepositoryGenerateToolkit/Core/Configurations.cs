using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.RepositoryGenerateToolkit.Utils;

namespace Alan.RepositoryGenerateToolkit.Core
{
    public class Configurations
    {
        public static Configurations Current { get; set; }

        static Configurations()
        {
            Current = Configurations.InitViaFile(@"E:\Projects\Alan.RepositoryGenerateToolkit\Alan.RepositoryGenerateToolkit\Template\configuration.json");
        }

        /// <summary>
        /// Sql connection string
        /// </summary>
        public string ConnectionString { get; set; }

        public RepositoryConfig Repository { get; set; }
        public ModelConfig Model { get; set; }

        #region Model
        public class RepositoryConfig
        {

            /// <summary>
            /// 通用IRepository名字
            /// </summary>
            public string GenericIRepositoryName { get; set; }
            /// <summary>
            /// IRepository命名空间
            /// </summary>
            public string IRepositoryNamespace { get; set; }
            /// <summary>
            /// IRepository保存路径
            /// </summary>
            public string IRepositorySavePath { get; set; }

            /// <summary>
            /// 通用Repository类名
            /// </summary>
            public string GenericRepositoryName { get; set; }

            /// <summary>
            /// Repository命名空间
            /// </summary>
            public string RepositoryNamespace { get; set; }

            /// <summary>
            /// Repository保存路径
            /// </summary>
            public string RepositorySavePath { get; set; }

            /// <summary>
            /// IRepository模板路径
            /// </summary>
            public string IRepositoryTemplatePath { get; set; }
            /// <summary>
            /// Repository模板路径
            /// </summary>
            public string RepositoryTemplatePath { get; set; }
            /// <summary>
            /// 通用Repository模板路径
            /// </summary>
            public string GenericRepositoryTemplatePath { get; set; }
            /// <summary>
            /// 通用IRepository模板路径
            /// </summary>
            public string GenericIRepositoryTemplatePath { get; set; }
        }


        public class ModelConfig
        {
            /// <summary>
            /// 模型模板路径
            /// </summary>
            public string ModelTemplatePath { get; set; }
            /// <summary>
            /// 模型文件保存路径
            /// </summary>
            public string ModelSavePath { get; set; }

            /// <summary>
            /// 模型命名空间
            /// </summary>
            public string ModelNamespace { get; set; }
            /// <summary>
            /// 模型上下文模板路径
            /// </summary>
            public string ContextTemplatePath { get; set; }
            /// <summary>
            /// Context文件保存路径
            /// </summary>
            public string ContextSavePath { get; set; }

            /// <summary>
            /// 模型上下文命名空间
            /// </summary>
            public string ContextNamespace { get; set; }
            /// <summary>
            /// 模型上下文类名
            /// </summary>
            public string ContextName { get; set; }
            /// <summary>
            /// web.config => ConnectionStrings
            /// </summary>
            public string ConnectionName { get; set; }
        }

        #endregion


        public override string ToString()
        {
            return this.ToJson();
        }
        private static Configurations InitViaJson(string json)
        {
            return json.ToModel<Configurations>();
        }
        private static Configurations InitViaFile(string fileFullPath)
        {
            var json = System.IO.File.ReadAllText(fileFullPath);
            return InitViaJson(json);
        }
    }
}
