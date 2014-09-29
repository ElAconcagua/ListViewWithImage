using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TabletMenu.Mobility.Controls
{
    public class ImageEx:Image, IDisposable
    {
        private TapGestureRecognizer tapGestureRecognizer;
        public event EventHandler Tapped;
        public ImageEx()
        {
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += tapGestureRecognizer_Tapped;
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void tapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Tapped != null)
                Tapped(sender, e);
        }


        public static readonly BindableProperty ImageUrlProperty = BindableProperty.Create<ImageEx, string>(p => p.ImageUrl, default(string));

        /// <summary>
        /// The URL of the image to display from the web
        /// </summary>
        public string ImageUrl
        {
           get { return (string)GetValue(ImageUrlProperty); }
           set { SetValue(ImageUrlProperty, value); }
        }

        public static readonly BindableProperty DefaultImageProperty = BindableProperty.Create<ImageEx, string>(p => p.DefaultImage, default(string));

        /// <summary>
        /// The path to the local image to display if the <c>ImageUrl</c> can't be loaded
        /// </summary>
        public string DefaultImage
        {
           get { return (string)GetValue(DefaultImageProperty); }
           set { SetValue(DefaultImageProperty, value); }
        }


        public static readonly BindableProperty LocalPathProperty = BindableProperty.Create<ImageEx, string>(p => p.LocalPath, default(string));

        /// <summary>
        /// The URL of the image to display from the web
        /// </summary>
        public string LocalPath
        {
           get { return (string)GetValue(LocalPathProperty); }
           set { SetValue(LocalPathProperty, value); }
        }

        public static readonly BindableProperty TypeLoadSourceProperty = BindableProperty.Create<ImageEx, ImageSourceType>(p => p.TypeLoadSource, ImageSourceType.File);

        /// <summary>
        /// The URL of the image to display from the web
        /// </summary>
        public ImageSourceType TypeLoadSource
        {
            get { return (ImageSourceType)GetValue(TypeLoadSourceProperty); }
            set { SetValue(TypeLoadSourceProperty, value); }
        }
        public bool IsLoadedWithDefault { get; set; }

        #region Dispose
        bool disposed;
        public void Dispose()
        {
            // Call our helper method.
            // Specifying "true" signifies that
            // the object user triggered the cleanup.
            Dispose(true);
            // Now suppress finalization.
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool disposing)
        {
            // Be sure we have not already been disposed!
            if (!disposed)
            {
                disposed = true;
                // If disposing equals true, dispose all
                // managed resources.
                if (disposing)
                {
                    tapGestureRecognizer.Tapped -= tapGestureRecognizer_Tapped;
                    if (Tapped != null)
                    {
                        foreach (var @delegate in Tapped.GetInvocationList())
                            Tapped -= (EventHandler)@delegate;
                    }
                    tapGestureRecognizer = null;

                }
                // Clean up unmanaged resources here.
            }
           
        }
        ~ImageEx()
        {
            // Call our helper method.
            // Specifying "false" signifies that
            // the GC triggered the cleanup.
            Dispose(false);
        }

        #endregion      
    }
}
