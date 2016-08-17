using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsDemo.Services;
using Xamarin.Forms;

namespace FormsDemo.Common.Views
{
    public class SampleGridView : View
    {
        public static BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource), typeof(ObservableCollection<Person>), typeof(SampleGridView), null);

        public ObservableCollection<Person> ItemSource
        {
            get { return (ObservableCollection<Person>) GetValue(ItemSourceProperty); }
            set
            {
                SetValue(ItemSourceProperty, value);
            }
        }
    }
}
