//#define grid
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabletMenu.Mobility.Controls;
using TabletMenu.Mobility;
using TabletMenu.Mobility.Templates;
using Xamarin.Forms;

namespace TabletMenu.Mobility.Templates
{
    public class SubCategoryCell : BaseViewCell
    {
        private StackLayout rootLayout;
        public SubCategoryCell()
        {
            rootLayout = new StackLayout
            {
                Padding= new Thickness(0,0,0,0),
                //Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var layout = new Grid
            {
               ColumnSpacing = 10,
               RowSpacing = 0,
               Padding = new Thickness(0, -6, 0, 7),
               HorizontalOptions = LayoutOptions.FillAndExpand,
               VerticalOptions = LayoutOptions.StartAndExpand,
               ColumnDefinitions = 
                {
                    new ColumnDefinition{ Width = new GridLength(35,GridUnitType.Star) },
                    new ColumnDefinition{ Width = new GridLength(65,GridUnitType.Star) }
                },
            };

            layout.Children.Add(ImageRenderer(), 0, 0);

            rootLayout.Children.Add(layout);
            View = rootLayout;
        }
        private Image ImageRenderer()
        {
            var image = new ImageEx
            {
                Aspect = Xamarin.Forms.Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                InputTransparent = true,
                WidthRequest = 280,
                HeightRequest = 185,
                LocalPath = "Images"
            };
            image.SetBinding(ImageEx.ImageUrlProperty, "Picture");
            return image;
        }
        protected override void DisposeOfInherited()
        {
            base.DisposeOfInherited();
        }

    }
}
