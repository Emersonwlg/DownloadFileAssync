using System;
using System.ComponentModel;
using System.Net;

namespace DownloadFileAssync
{
    public class Program
    {
        public static void Main()
        {
            DownloadFile();
        }

        public static void DownloadFile()
        {
            string url = "http://ec.europa.eu/economy_finance/db_indicators/surveys/documents/series/nace2_ecfin_1409/all_surveys_total_sa_nace2.zip";
            WebClient downloader = new WebClient();
            downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloader_DownloadFileCompleted);
            downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Downloader_DownloadProgressChanged);
            downloader.DownloadFileAsync(new Uri(url), "C:\\arquivoTeste.zip");

            Console.ReadKey();
        }

        static void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine(e.BytesReceived + " " + e.ProgressPercentage);
        }

        static void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                Console.WriteLine(e.Error.Message);
            else
                Console.WriteLine("Download Completed!!!");
        }
    }

    public class DownloadManager
    {
        public void DownloadFile(string sourceUrl, string targetFolder)
        {
            WebClient downloader = new WebClient();
            // fake as if you are a browser making the request.
            downloader.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
            downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloader_DownloadFileCompleted);
            downloader.DownloadProgressChanged +=
                new DownloadProgressChangedEventHandler(Downloader_DownloadProgressChanged);
            downloader.DownloadFileAsync(new Uri(sourceUrl), targetFolder);
            // wait for the download to complete
            while (downloader.IsBusy) { }
        }

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // print progress of download.
            Console.WriteLine(e.BytesReceived + " " + e.ProgressPercentage);
        }

        private void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // display completion status.
            if (e.Error != null)
                Console.WriteLine(e.Error.Message);
            else
                Console.WriteLine("Download Completed!!!");
        }
    }
}
