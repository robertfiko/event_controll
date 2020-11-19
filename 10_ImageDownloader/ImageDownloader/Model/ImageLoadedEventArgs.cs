using System;

namespace ImageDownloader.Model
{
    internal class ImageLoadedEventArgs : EventArgs
    {
        public WebImage image { get; private set; }

        public ImageLoadedEventArgs(WebImage image)
        {
            this.image = image;
        }
    }
}