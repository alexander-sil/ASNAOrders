using ASNAOrders.Web.ConfigServiceExtensions;
using ASNAOrders.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using Serilog;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to watch for new and existing images uploaded to {StaticConfig.XMLStockPath}\images directory.
    /// </summary>
    public class ImageWatcherService : IDisposable, IHostedService
    {
        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private FileSystemWatcher Watcher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ILogger<ImageWatcherService> Logger { get; set; }

        /// <summary>
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="contextFactory">Database context to write to. Stocks table is used.</param>
        public ImageWatcherService(IDbContextFactory<ASNAOrdersDbContext> contextFactory, ILogger<ImageWatcherService> logger)
        {
            Context = contextFactory.CreateDbContext();
            Logger = logger;
        }

        private void OnUpload(object sender, FileSystemEventArgs e)
        {
            using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.ASNATinystashUploadUri);
            using HttpClient client = new HttpClient();

            message.Content = new ByteArrayContent(System.IO.File.ReadAllBytes(e.FullPath));

            message.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(e.FullPath));
            message.Content.Headers.ContentLength = new FileInfo(e.FullPath).Length;

            message.Headers.Add(Properties.Resources.ASNAAppIdKey, Properties.Resources.ASNAAppIdValue);
            Thread.Sleep(250);

            HttpResponseMessage response = client.Send(message);
            string url = response.EnsureSuccessStatusCode().Content.ToString();

            System.IO.File.AppendAllText(Path.Combine(Program.ImagePath, Properties.Resources.ASNAImageListPath), $"{Regex.Replace(e.Name, new FileInfo(e.FullPath).Extension, string.Empty)}\t{url}{Environment.NewLine}");

            #if DEBUG
            Log.Information($"Image {e.Name} hosted at Tinystash URI {url}");
            #endif
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void DoWork()
        {


            if (new FileInfo(Path.Combine(Program.ImagePath, Properties.Resources.ASNAImageListPath)).Length > 0)
            {
                foreach (FileInfo file in new DirectoryInfo(Program.ImagePath).EnumerateFiles())
                {
                    using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.ASNATinystashUploadUriAlt);
                    using HttpClient client = new HttpClient();

                    message.Content = new ByteArrayContent(System.IO.File.ReadAllBytes(file.FullName));

                    message.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(file.FullName));
                    message.Content.Headers.ContentLength = file.Length;

                    message.Headers.Add(Properties.Resources.ASNAAppIdKey, Properties.Resources.ASNAAppIdValue);
                    Thread.Sleep(250);

                    HttpResponseMessage response = client.Send(message);
                    string url = response.EnsureSuccessStatusCode().Content.ToString();

                    System.IO.File.AppendAllText(Path.Combine(Program.ImagePath, Properties.Resources.ASNAImageListPath), $"{Regex.Replace(file.Name, file.Extension, string.Empty)}\t{url}{Environment.NewLine}");

                    #if DEBUG
                    Log.Information($"Image {file.Name} uploaded and hosted at Tinystash URI {url}");
                    #endif
                }
            }


            Watcher = new FileSystemWatcher(Program.ImagePath);

            Watcher.Created += OnUpload;
        }

        /// <summary>
        /// Gets the Tinystash.undef.im image link for the specified product.
        /// </summary>
        /// <param name="unicode">NNT of the product AKA image filename</param>
        /// <returns>Tinystash.undef.im URL for the concrete product image.</returns>
        public static string GetImageTinystash(string unicode)
        {
            string[] data = System.IO.File.ReadAllLines(Path.Combine(Program.ImagePath, Properties.Resources.ASNAImageListPath));

            foreach (string line in data)
            {
                if (line.Contains(unicode))
                {
                    return line.Split("\t")[1].Trim();
                }
            }

            return Properties.Resources.ASNADefaultCategoryPlaceholder;
        }

        /// <summary>
        /// Gets the SHA-1 cryptographic function result for the specified image.
        /// </summary>
        /// <param name="unicode">NNT of the product AKA image filename</param>
        /// <returns>SHA-1 hash contained in hexadecimal string form.</returns>
        public static string GetImageSha1(string unicode)
        {
            foreach (FileInfo file in new DirectoryInfo(Program.ImagePath).EnumerateFiles())
            {
                if (file.Name.Contains(unicode))
                {
                    return Convert.ToHexString(SHA1.Create().ComputeHash(System.IO.File.ReadAllBytes(file.FullName)));
                }
            }

            return Properties.Resources.DefaultHashString;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information($"Started image watcher service at {DateTime.Now}");

            if (!System.IO.File.Exists(Path.Combine(Program.ImagePath, Properties.Resources.ASNAImageListPath)))
            {
                System.IO.File.Create(Path.Combine(Program.ImagePath, Properties.Resources.ASNAImageListPath)).Dispose();
            }

            return Task.Run(DoWork);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
