using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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

        public ReactiveProperty<Color> ResultColor { get; }

        public ObservableViewModelDemoViewModel()
        {
            Red = new ReactiveProperty<double>();
            Green = new ReactiveProperty<double>();
            Red = new ReactiveProperty<double>();

            ResultColor = Observable.CombineLatest(Red, Green, Blue, (r, g, b) => Color.FromRgb(r, g, b)).ToReactiveProperty();
        }
    }
}
