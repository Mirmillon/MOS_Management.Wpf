using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOS_Management.API.Models;
using MOS_Management.API.RepositoryInterface;
using MOS_Management.Wpf.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MOS_Management.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            //ServiceProvider.GetServices<CodePage>();
            //ServiceProvider.GetServices<NomenclaturePage>();
            mainWindow.Show();
        }


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MOS_Communes_DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MosConnection")));
            services.AddTransient(typeof(App));
            services.AddTransient(typeof(MainWindow));
           // services.AddTransient(typeof(CodePage));
            //services.AddTransient(typeof(NomenclaturePage));
            services.AddScoped<IAgenceRepository, AgenceRepository>();
            services.AddScoped<INomenclatureRepository, NomenclatureRepository>();
            services.AddScoped<ICodeRepository, CodeRepository>();

        }
    }
}
