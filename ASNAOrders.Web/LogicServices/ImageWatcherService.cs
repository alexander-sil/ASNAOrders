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

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to watch for new and existing images uploaded to {StaticConfig.XMLStockPath}\images directory.
    /// </summary>
    public class ImageWatcherService
    {
        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FileSystemWatcher Watcher { get; set; }

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
            using var context = contextFactory.CreateDbContext();
            Context = context;

            Logger.LogInformation($"Started ImageWatcherService at {DateTime.Now}");

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Logger.LogInformation($"XML directory created at {path}. Please point the FTP server to this exact folder.");
            }

            string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImagesPath);

            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
                Logger.LogInformation($"Images directory created at {path1}");
            }

            string path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImageListPath);

            if (!System.IO.File.Exists(path2))
            {
                System.IO.File.Create(path2).Dispose();
            }

            if (new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImageListPath)).Length == 0)
            {
                foreach (FileInfo file in new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImagesPath)).EnumerateFiles())
                {
                    using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.ASNATinystashUploadUriAlt);
                    using HttpClient client = new HttpClient();

                    message.Content = new ByteArrayContent(System.IO.File.ReadAllBytes(file.FullName));

                    message.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(file.FullName));
                    message.Content.Headers.ContentLength = file.Length;

                    message.Headers.Add(Properties.Resources.ASNAAppIdKey, Properties.Resources.ASNAAppIdValue);

                    HttpResponseMessage response = client.Send(message);
                    string url = response.EnsureSuccessStatusCode().Content.ToString();

                    System.IO.File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImageListPath), $"{Regex.Replace(file.Name, file.Extension, string.Empty)}\t{url}{Environment.NewLine}");
                }
            }


            Watcher = new FileSystemWatcher(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImagesPath));

            Watcher.Created += OnUpload;
        }

        private void OnUpload(object sender, FileSystemEventArgs e)
        {
            using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.ASNATinystashUploadUri);
            using HttpClient client = new HttpClient();

            message.Content = new ByteArrayContent(System.IO.File.ReadAllBytes(e.FullPath));

            message.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(e.FullPath));
            message.Content.Headers.ContentLength = new FileInfo(e.FullPath).Length;

            message.Headers.Add(Properties.Resources.ASNAAppIdKey, Properties.Resources.ASNAAppIdValue);

            HttpResponseMessage response = client.Send(message);
            string url = response.EnsureSuccessStatusCode().Content.ToString();

            System.IO.File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImageListPath), $"{Regex.Replace(e.Name, new FileInfo(e.FullPath).Extension, string.Empty)}\t{url}{Environment.NewLine}");
        }

        /// <summary>
        /// Gets the Tinystash.undef.im image link for the specified product.
        /// </summary>
        /// <param name="unicode">NNT of the product AKA image filename</param>
        /// <returns>Tinystash.undef.im URL for the concrete product image.</returns>
        public static string GetImageTinystash(string unicode)
        {
            string[] data = System.IO.File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImageListPath));

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
            foreach (FileInfo file in new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath, Properties.Resources.ASNAImagesPath)).EnumerateFiles())
            {
                if (file.Name.Contains(unicode))
                {
                    return Convert.ToHexString(SHA1.Create().ComputeHash(System.IO.File.ReadAllBytes(file.FullName)));
                }
            }

            return Properties.Resources.DeadfaceString;
        }
    }
}
