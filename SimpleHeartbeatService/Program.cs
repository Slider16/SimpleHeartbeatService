using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SimpleHeartbeatService
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Heartbeat>(service =>
                {
                    service.ConstructUsing(heartbeat => new Heartbeat());
                    service.WhenStarted(heartbeat => heartbeat.Start());
                    service.WhenStopped(heartbeat => heartbeat.Stop());
                });

                x.RunAsLocalSystem();
                //x.RunAs("username", "password");

                x.SetServiceName("HeartbeatService");
                x.SetDisplayName("Heartbeat Service");
                x.SetDescription("This is the sample heartbeat service used in Tim Corey's YouTube demo.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
