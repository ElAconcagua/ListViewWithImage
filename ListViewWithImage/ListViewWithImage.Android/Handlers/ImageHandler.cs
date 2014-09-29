
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using System.Net;
using Android.Webkit;
using System.IO;
using Runtime.Kernel.Core;
using Runtime.Kernel.Droid.Services;
using Runtime.Kernel.Services;


namespace Runtime.Kernel.Droid.Handler
{
    public sealed class DroidImageHandler
    {
        private readonly Android.Content.Context context;
        private string fileDirectory;
        private string FileDirectory
        {
            get
            {
                if (fileDirectory == null)
                {
                    fileDirectory = context.FilesDir.Path;           
                }
                return fileDirectory;
            }
        }
        public DroidImageHandler(Android.Content.Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets a <see cref="Bitmap"/> for the supplied <see cref="ImageSource"/>.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource"/> to get the image for.</param>
        /// <returns>A loaded <see cref="Bitmap"/>.</returns>
        public async Task<Bitmap> GetBitmapAsync(ImageSource source)
        {
            var handler = GetHandler(source);
            var returnValue = (Bitmap)null;

            returnValue = await handler.LoadImageAsync(source, context);

            return returnValue;
        }
        /// <summary>
        /// Returns the proper <see cref="IImageSourceHandler"/> based on the type of <see cref="ImageSource"/> provided.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource"/> to get the handler for.</param>
        /// <returns>The needed handler.</returns>
        private IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        #region Synchronous
        public Bitmap GetImageLocalCache(string imageUrl, string localPath,
           string defaultImage)
        {
            Bitmap toReturn = GetImageLocalFromFile(imageUrl, localPath, context);
            if (!toReturn.ToMaybe().HasValue)
            {
                if (IsReachableAndUrlValid(imageUrl))
                {
                    var imageInBytes = DownloadDataFromWeb(imageUrl);
                    if (imageInBytes.ToMaybe().HasValue && imageInBytes.Length > 0)
                    {
                        var imagelocal = UrlToLocal(imageUrl, localPath, context);
                        CopyInLocal(imageInBytes, imagelocal);
                        toReturn = ConvertBytesToBitmap(imageInBytes);
                    }
                }
                else
                {
                    toReturn = GetImageLocalFromResource(defaultImage);
                    //imageEx.IsLoadedWithDefault = true;
                }
            }

            return toReturn;
        }

        public Bitmap GetImageLocalFromFile(string imageUrl, string localPath,
            Android.Content.Context context)
        {
            Bitmap toReturn = null;
            if (!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrEmpty(localPath))
            {
                var imageLocal = UrlToLocal(imageUrl, localPath, context);

                if (System.IO.File.Exists(imageLocal))
                {
                    toReturn = Android.Graphics.BitmapFactory.DecodeFile(imageLocal);
                }
            }
            return toReturn;
        }
        private byte[] DownloadDataFromWeb(string url)
        {
            byte[] toReturn = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    toReturn = webClient.DownloadData(url);
                }
            }
            catch (System.Exception ex)
            {
                //si hay un error y no pudo cargar pues no se carge.
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return toReturn;
        }
        private void CopyInLocal(byte[] imageInBytes, string pathFile)
        {
            using (FileStream fileStream = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                fileStream.Write(imageInBytes, 0, (int)imageInBytes.Length);
            }
        }
        private Bitmap ConvertBytesToBitmap(byte[] imageInBytes)
        {
            Bitmap toReturn = null;
            if (imageInBytes != null && imageInBytes.Length > 0)
            {
                toReturn = Android.Graphics.BitmapFactory.DecodeByteArray(imageInBytes, 0, imageInBytes.Length);
            }
            return toReturn;
        }
        public Bitmap GetImageLocalFromResource(string imageUrl)
        {
            Bitmap toReturn = null;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var imageLocal = System.IO.Path.GetFileNameWithoutExtension(imageUrl);
                //int Resxid = Context.Resources.GetIdentifier(imageLocal, "drawable", "com.tabletmenu.Droid");
                int Resxid = context.Resources.GetIdentifier(imageLocal, "drawable", context.PackageName);
                if (Resxid > 0)
                {
                    try
                    {
                        toReturn = Android.Graphics.BitmapFactory.DecodeResource(context.Resources, Resxid);
                    }
                    catch (System.Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            return toReturn;
        }
        #endregion

        #region Asynchronous
        public async Task<Bitmap> GetImageLocalCacheAsync(string imageUrl, string localPath,
            string defaultImage)
        {
            Bitmap toReturn = await GetImageLocalFromFileAsync(imageUrl, localPath, context);
            if (!toReturn.ToMaybe().HasValue)
            {
                if (IsReachableAndUrlValid(imageUrl))
                {
                    var imageInBytes = await DownloadDataFromWebAsync(imageUrl);
                    if (imageInBytes.ToMaybe().HasValue && imageInBytes.Length > 0)
                    {
                        var imagelocal = UrlToLocal(imageUrl, localPath, context);
                        await CopyInLocalAsync(imageInBytes, imagelocal);
                        toReturn = await ConvertBytesToBitmapAsync(imageInBytes);
                    }
                }
                else
                {
                    toReturn = await GetImageLocalFromResourceAsync(defaultImage);
                    //imageEx.IsLoadedWithDefault = true;
                }
            }

            return toReturn;
        }

        public async Task<Bitmap> GetImageLocalFromFileAsync(string imageUrl, string localPath,
             Android.Content.Context context)
        {
            Bitmap toReturn = null;
            if (!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrEmpty(localPath))
            {
                var imageLocal = UrlToLocal(imageUrl, localPath, context);

                if (System.IO.File.Exists(imageLocal))
                {
                    toReturn = await Android.Graphics.BitmapFactory.DecodeFileAsync(imageLocal);
                }
            }
            return toReturn;
        }

        public async Task<Bitmap> GetImageLocalFromResourceAsync(string imageUrl)
        {
            Bitmap toReturn = null;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var imageLocal = System.IO.Path.GetFileNameWithoutExtension(imageUrl);
                //int Resxid = Context.Resources.GetIdentifier(imageLocal, "drawable", "com.tabletmenu.Droid");
                int Resxid = context.Resources.GetIdentifier(imageLocal, "drawable", context.PackageName);
                if (Resxid > 0)
                {
                    try
                    {
                        toReturn = await Android.Graphics.BitmapFactory.DecodeResourceAsync(context.Resources, Resxid);
                    }
                    catch (System.Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            return toReturn;
        }

        public async Task<Bitmap> GetImageWebAsync(string url)
        {
            Bitmap toReturn = null;
            if (IsReachableAndUrlValid(url))
            {
                toReturn = await GetImageFromWebAsync(url);
            }
            return toReturn;
        }
        private async Task<Bitmap> GetImageFromWebAsync(string url)
        {
            Bitmap imageBitmap = null;
            try
            {
                var imageBytes = await DownloadDataFromWebAsync(url);
                imageBitmap = await ConvertBytesToBitmapAsync(imageBytes);
            }
            catch (System.Exception ex)
            {
                //si hay un error y no pudo cargar pues no se carge.
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return imageBitmap;
        }
        private bool IsReachableAndUrlValid(string url)
        {
            var networkStatus = Reachability.InternetConnectionStatus();
            var isReachable = networkStatus != NetworkStatus.NotReachable;
            return isReachable && url.IsUrl();
        }
        private async Task<byte[]> DownloadDataFromWebAsync(string url)
        {
            byte[] toReturn = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    toReturn = await webClient.DownloadDataTaskAsync(url);
                }
            }
            catch (System.Exception ex)
            {
                //si hay un error y no pudo cargar pues no se carge.
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return toReturn;
        }

        private string UrlToLocal(string imageUrl, string localPath, Android.Content.Context context)
        {
            return FileDirectory + "/" + localPath.Replace('\\', Java.IO.File.SeparatorChar) + "/" + System.IO.Path.GetFileName(imageUrl);
        }
        private async Task CopyInLocalAsync(byte[] imageInBytes, string pathFile)
        {
            using (FileStream fileStream = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                await fileStream.WriteAsync(imageInBytes, 0, (int)imageInBytes.Length);
            }
        }

        private async Task<Bitmap> ConvertBytesToBitmapAsync(byte[] imageInBytes)
        {
            Bitmap toReturn = null;
            if (imageInBytes != null && imageInBytes.Length > 0)
            {
                toReturn = await Android.Graphics.BitmapFactory.DecodeByteArrayAsync(imageInBytes, 0, imageInBytes.Length);
            }
            return toReturn;
        }
        #endregion
    }
}