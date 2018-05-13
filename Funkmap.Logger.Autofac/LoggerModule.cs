using System;
using System.IO;
using System.Reflection;
using Autofac;
using Funkmap.Common.Settings;
using NLog;
using NLog.Config;
using Module = Autofac.Module;

namespace Funkmap.Logger.Autofac
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(container =>
            {
                var settingsService = container.Resolve<ISettingsService>();
                var settings = settingsService.GetSettings();


                string root = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                var emailConfig = Path.Combine(root, "NLogEmail.config");
                var fileConfig = Path.Combine(root, "NLog.config");

                string logConfig;

                switch (settings.LoggingType)
                {
                    case LoggingType.Empty:
                        logConfig = "";
                        break;

                    case LoggingType.File:
                        logConfig = fileConfig;
                        break;

                    case LoggingType.Email:
                        logConfig = emailConfig;
                        break;
                    default:
                        logConfig = fileConfig;
                        break;
                }

                if (!String.IsNullOrEmpty(logConfig)) LogManager.Configuration = new XmlLoggingConfiguration(logConfig);
                var logger = LogManager.GetCurrentClassLogger();
                return logger;
            }).SingleInstance().As<ILogger>();

            builder.RegisterGeneric(typeof(FunkmapLogger<>)).As(typeof(IFunkmapLogger<>));

            base.Load(builder);
        }
    }
}
