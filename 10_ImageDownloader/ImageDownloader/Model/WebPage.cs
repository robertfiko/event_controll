using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader.Model
{
    class WebPage
    {
        public List<WebImage> Images { get; private set; }
        public Uri BaseUrl { get; private set; }

        public event EventHandler<ImageLoadedEventArgs> ImageLoaded;
        public event EventHandler<LoadProgressEventArgs> LoadProgress;

        public WebPage(Uri url)
        {
            if (url != null && url.IsAbsoluteUri)
                BaseUrl = url;
            else throw new ArgumentException();

            Images = new List<WebImage>();
        }

        public async Task LoadImagesAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(BaseUrl);
            var content = await response.Content.ReadAsStringAsync();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(content);
            var nodes = doc.DocumentNode.SelectNodes("//img");

            int ImageCount = 0;
            foreach (var node in nodes)
            {
                if (!node.Attributes.Contains("src"))
                    continue;
                
                Uri imageUrl = new Uri(node.Attributes["src"].Value, UriKind.RelativeOrAbsolute);
                if (!imageUrl.IsAbsoluteUri)
                    imageUrl = new Uri(BaseUrl, imageUrl);
                

                try
                {
                    var image = await WebImage.DownloadAsync(imageUrl);
                    Images.Add(image);
                    ImageCount++;
                    DOImageLoaded(image);
                    DOLoadProgress((int)(100 * (ImageCount / (double)nodes.Count)));



                }
                catch
                {
                    // ignored
                }
            }




        }

        private void DOImageLoaded(WebImage image)
        {
            ImageLoaded?.Invoke(this, new ImageLoadedEventArgs(image));
        }

        private void DOLoadProgress(int progress)
        {
            LoadProgress?.Invoke(this, new LoadProgressEventArgs(progress));
        }
    }
}

