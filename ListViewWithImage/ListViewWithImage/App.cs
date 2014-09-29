using ListViewWithImage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabletMenu.Mobility.Entities;
using TabletMenu.Mobility.Templates;
using Xamarin.Forms;

namespace ListViewWithImage
{
    public class App
    {
        public static Page GetMainPage()
        {
            var page = new ContentPage();
            var menuList = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Name"),
                GroupShortNameBinding = new Binding("Name"),
                GroupHeaderTemplate = new DataTemplate(typeof(SubCategoryHeaderCell)),
                HasUnevenRows = true,
                BackgroundColor = Color.White,
                ItemTemplate = new DataTemplate(typeof(SubCategoryCell)),
                ItemsSource = GetDatas()
            };

            page.Content = menuList;
            return page;
        }

        private static IEnumerable<SubCategoryEntity> GetDatas()
        {
            IList<SubCategoryEntity> datas = new List<SubCategoryEntity>();

            datas.Add(new SubCategoryEntity("Category_1", new ArticleDTOM[]
                {
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_entradas_ribbon.png" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_entradas_frame.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_sopas_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_ensaladas_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_pastas_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_pescados_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_carnes_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_postres_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_bebidas_ribbon.jpg" },
                }));
            datas.Add(new SubCategoryEntity("Category_2", new ArticleDTOM[]
                {
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_entradas_ribbon.png" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/menu_menu_ribbon.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/lomo_a_la_mostaza_800_476.png" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/conejo_a_la_mostaza_800_476.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/pollo_a_la_crema_de_verdeo_800_476.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/conejo_a_la_mostaza_800_476.pjg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/ojo_de_bife_a_la_pimienta_800_476.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/carre_de_cerdo_800_476.jpg" },
                    new ArticleDTOM{ Picture = "http://tabletmenu.net/ImageMock/agua_sin_gas_800_476.jpg" },
                }));
            
            return datas;
        }

    }
}
