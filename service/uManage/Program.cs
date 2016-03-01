using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Configuration;
namespace S203.uManage
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.StartLogging();
            var logger = LogManager.GetLogger("umanage-server");

            try
            {
                logger.Info("Starting uManage Server");

                var baseAddress = ConfigurationManager.AppSettings["baseUri"];

                if (string.IsNullOrWhiteSpace(baseAddress))
                    throw new ArgumentNullException(baseAddress, "Base address not specified, check app.config and ensure the baseUri appSetting is specified.");

                logger.Info("Base address set: {0}", baseAddress);

                logger.Info("Starting web server...");

                using (WebApp.Start<Startup>(baseAddress))
                {
                    logger.Info("Web server is ready to serve!");

                    if (Environment.UserInteractive)
                        logger.Info("Press any key to exit... ");

                    Console.Read();

                    logger.Warn("Stopping server...");
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Unhandled error, server has terminated!");
            }
            finally
            {
                logger.Info("uManage is stopped!");
            }
        }
    }
}
