using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TabletMenu.Mobility.Templates
{
    public class BaseViewCell : ViewCell, IDisposable
    {
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
                    DisposeOfInherited();
                }
                // Clean up unmanaged resources here.
            }
           
        }
        ~BaseViewCell()
        {
            // Call our helper method.
            // Specifying "false" signifies that
            // the GC triggered the cleanup.
            Dispose(false);
        }
        protected virtual void DisposeOfInherited() { }
        #endregion      	

    }
}
