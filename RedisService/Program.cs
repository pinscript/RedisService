using System.Configuration;
using Topshelf;

namespace RedisService
{
    public class Program
    {
        public static void Main(string[] argv)
        {
            HostFactory.Run(x =>
            {
                x.Service<RedisServerWrapper>(s =>
                {
                    var pathToRedisExecutable = ConfigurationManager.AppSettings["PathToRedisExecutable"];

                    s.ConstructUsing(_ => new RedisServerWrapper(pathToRedisExecutable));
                    s.WhenStarted(rsw => rsw.Start());
                    s.WhenStopped(rsw => rsw.Stop());
                    s.WhenShutdown(rsw => rsw.Stop());
                   
                });

                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.SetDescription("Wraps redis-server.exe to avoid having a console application running for a specific user.");
                x.SetDisplayName("RedisService");
                x.SetServiceName("RedisService");
            });
        }        
    }
}