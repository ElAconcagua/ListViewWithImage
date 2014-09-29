using ListViewWithImage.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletMenu.Mobility.Entities
{
    public class SubCategoryEntity : ObservableCollection<ArticleDTOM>
    {
        public SubCategoryEntity(string name, IEnumerable<ArticleDTOM> collection)
            : base(collection)
		{
            this.Name = name;
		}
        public string Name
		{
			get;
			private set;
		}
    }
}
