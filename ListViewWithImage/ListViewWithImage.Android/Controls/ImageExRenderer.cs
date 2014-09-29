using Android.Graphics;
using System.Net;
using System.Threading.Tasks;
using TabletMenu.Mobility.Controls;
using TabletMenu.Mobility.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.IO;
using System.Collections.Specialized;
using Runtime.Kernel.Droid.Handler;

[assembly: ExportRenderer(typeof(ImageEx), typeof(ImageExRenderer))]
namespace TabletMenu.Mobility.Droid.Controls
{
    public class ImageExRenderer : ImageRenderer
    {
        protected async override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                try
                {
                    await SetImageAsync(this.Control, (ImageEx)Element);
                }
                catch(System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            Bind((ImageEx)e.NewElement);
        }

        private void Bind(ImageEx newElement)
        {
            if (newElement != null)
            {
                newElement.PropertyChanged += newElement_PropertyChanged;
            }
        }

        async void newElement_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ImageUrl")
            {
                await SetImageAsync(this.Control, (ImageEx)Element);
            }
        }

        private bool IsUpdate(ElementChangedEventArgs<Image> e)
        {
            bool toReturn = false;

            if (e.OldElement == null)
                toReturn = true;
            else
            {
                var ImageUrlOld = ((ImageEx)e.OldElement).ImageUrl;
                var ImageUrlNew = ((ImageEx)e.NewElement).ImageUrl;

                return ImageUrlOld != ImageUrlNew;
            }
            return toReturn;
        }
        private async Task SetImageAsync(Android.Widget.ImageView control, ImageEx imageEx)
        {
            Bitmap image = null;
            if (string.IsNullOrEmpty(imageEx.ImageUrl))
            {
                image = await ImageHandler.GetImageLocalFromResourceAsync(imageEx.DefaultImage);
                imageEx.IsLoadedWithDefault = true;
            }
            else
            {
                switch(imageEx.TypeLoadSource)
                {
                    case ImageSourceType.File:
                        image = await ImageHandler.GetImageLocalCacheAsync(imageEx.ImageUrl, imageEx.LocalPath, imageEx.DefaultImage);
                        break;
                    case ImageSourceType.Web:
                        image = await ImageHandler.GetImageWebAsync(imageEx.ImageUrl);
                        break;
                    case ImageSourceType.FileResource:
                        image = await ImageHandler.GetImageLocalCacheAsync(imageEx.ImageUrl, imageEx.LocalPath, imageEx.DefaultImage);
                        if(image == null)
                            image = await ImageHandler.GetImageLocalFromResourceAsync(imageEx.ImageUrl);
                        break;
                    case ImageSourceType.Resource:
                        image = await ImageHandler.GetImageLocalFromResourceAsync(imageEx.ImageUrl);
                        break;
                    default:
                        image = await ImageHandler.GetImageLocalFromResourceAsync(imageEx.DefaultImage);
                        imageEx.IsLoadedWithDefault = true;
                        break;

                }
            }
            //imageEx.Source = ImageSource.FromStream(()=> )
            control.SetImageBitmap(image);
        }

        private DroidImageHandler imageHandler;
        private DroidImageHandler ImageHandler
        {
            get 
            { 
                if(imageHandler == null)
                    imageHandler = new DroidImageHandler(Context);
                return imageHandler; 
            }
        }
    }
}