using System;
using System.ComponentModel;
using System.Net;

namespace dnget
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Enter source url: ");
            string remoteUri = Console.ReadLine();
            Console.WriteLine("Enter file.name: ");
            string fileName = Console.ReadLine();
            WebClient myWebClient = new WebClient();
            Console.WriteLine($"\nDownloading File \"{fileName}\" to {Environment.CurrentDirectory}\nfrom \"{remoteUri}\"\n");
            myWebClient.DownloadFileCompleted += Completed;
            myWebClient.DownloadProgressChanged += ProgressChanged;
            myWebClient.DownloadFileTaskAsync(new Uri(remoteUri), fileName).Wait();
            Console.ReadKey();
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"\rDownloaded {BytesToString(e.BytesReceived)}    ");
            System.Threading.Thread.Sleep(1000);
        }

        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                Console.WriteLine($"\nDownload task completed.");
            }
        }

        private static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num) + suf[place];
        }
    }
}