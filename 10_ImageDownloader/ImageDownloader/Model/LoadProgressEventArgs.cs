using System;

namespace ImageDownloader.Model
{
    internal class LoadProgressEventArgs : EventArgs
    {
        public int progress { get; private set; }

        public LoadProgressEventArgs(int progress)
        {
            this.progress = progress;
        }
    }
}