using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.RepositoryGenerateToolkit.Core;

namespace Alan.RepositoryGenerateToolkit.Models
{
    class ModelContext : DataContext
    {
        public ModelContext(): base(Configurations.Current.ConnectionString){ }
        
        public ModelContext(string fileOrServerOrConnection)
             : base(fileOrServerOrConnection)
        {
        }

        public ModelContext(string fileOrServerOrConnection, System.Data.Linq.Mapping.MappingSource mapping)
            : base(fileOrServerOrConnection, mapping)
        {
        }

        public ModelContext(IDbConnection connection)
            : base(connection)
        {
        }

        public ModelContext(IDbConnection connection, System.Data.Linq.Mapping.MappingSource mapping)
            : base(connection, mapping)
        {
        }
    }
}
