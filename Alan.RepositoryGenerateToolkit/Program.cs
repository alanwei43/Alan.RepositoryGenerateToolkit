using Alan.RepositoryGenerateToolkit.Core;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Configuration;

namespace Alan.RepositoryGenerateToolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelGenerate.SaveModels();
            //RepositoryGenerate.SaveRepositories();
        }
    }
}
