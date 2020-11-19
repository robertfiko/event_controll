using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader.Model
{
    class WebImage
    {
        public Uri url {get; private set; }
        public Image image { get; private set; }

        public WebImage(Uri u, Image i)
        {
            url = u;
            image = i;
        }

        public static async Task<WebImage> DownloadAsync(Uri url)
        {
            if (url != null && url.IsAbsoluteUri)
            {
                HttpClient client = new HttpClient();
                Stream stream = await client.GetStreamAsync(url);
                Image image = Image.FromStream(stream);
                return new WebImage(url, image);
            }
            else throw new Exception("URL is not valid");
            
        }
    }
}
