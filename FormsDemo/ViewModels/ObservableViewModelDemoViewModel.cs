using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using Reactive.Bindings;
using Xamarin.Forms;

namespace FormsDemo.ViewModels
{
    public class ObservableViewModelDemoViewModel : BindableBase
    {
        public ReactiveProperty<double> Red { get; }
        public ReactiveProperty<double> Green { get; }
        public ReactiveProperty<double> Blue { get; }

        public ReactiveProperty<string> RedValue { get; }

        public ReactiveProperty<Color> ResultColor { get; }

        public ObservableViewModelDemoViewModel()
        {
            Red = new ReactiveProperty<double>(0);
            Green = new ReactiveProperty<double>(0);
            Blue = new ReactiveProperty<double>(0);

            ResultColor = Observable.CombineLatest(Red, Green, Blue, (r, g, b) =>
            {
                var color = Color.FromRgb(r, g, b);;
                Debug.WriteLine($"{color.ToString()}");
                return color;
                
            }).ToReactiveProperty();
        }
    }
}
