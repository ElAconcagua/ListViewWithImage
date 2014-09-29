using TabletMenu.Mobility.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TabletMenu.Mobility;

namespace TabletMenu.Mobility.Templates
{
    public class SubCategoryHeaderCell : BaseViewCell
    {
        public SubCategoryHeaderCell()
        {
            var rootLayout = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength(1,GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition{ Height=GridLength.Auto },
                    new RowDefinition{ Height=GridLength.Auto },
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            var label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Font= Font.SystemFontOfSize(20,FontAttributes.Bold),               
                TextColor = Color.Black
            };
            label.SetBinding(Label.TextProperty, "BindingContext.Name");

            var contentLabel = new ContentView
            {
                BindingContext = this,
                Padding= new Thickness(10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = label

            };
            rootLayout.Children.Add(contentLabel, 0, 0);
            View = rootLayout;
        }
    }
}
