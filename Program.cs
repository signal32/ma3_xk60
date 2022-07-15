using System;
using System.Threading;
using System.Threading.Tasks;

namespace ma3_xk60.net
{
    internal class Program
    {
        
        private static readonly CancellationTokenSource CancellationToken = new CancellationTokenSource();
        
        public static async Task Main(string[] args)
        {
            var xkeys = new XK_60_80.Xk60_80_();
            SetupCancellation();
            
            Console.Out.WriteLine("Welcome. Detected {0}", xkeys.ConnectedDevices[0].ProductString);
            
            xkeys.ButtonChange += eventArgs =>
            {
                Console.WriteLine("foo");
            };

            await ExitRequest();
            Console.WriteLine("Exiting...");
        }

        static void SetupCancellation()
        {
            Console.CancelKeyPress += (sender, args) =>
            {
                Console.WriteLine("Operation cancelled");
                CancellationToken.Cancel();
                args.Cancel = true;
            };
        }

        async static Task ExitRequest()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }
    }
}