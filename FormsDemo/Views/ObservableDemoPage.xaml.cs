using System;
using System.Reactive.Linq;
using System.Threading;
using FormsDemo.Services;
using Xamarin.Forms;

namespace FormsDemo.Views
{
    public partial class ObservableDemoPage : ContentPage
    {
        public ObservableDemoPage()
        {
            InitializeComponent();
            var redValue = Observable.FromEventPattern<EventHandler<ValueChangedEventArgs>, ValueChangedEventArgs>(eh => RedSlider.ValueChanged += eh, eh => RedSlider.ValueChanged -= eh)
                .Select(x => x.EventArgs.NewValue)
                .StartWith(RedSlider.Value);

            var greenValue = Observable.FromEventPattern<EventHandler<ValueChangedEventArgs>, ValueChangedEventArgs>(eh => GreenSlider.ValueChanged += eh, eh => GreenSlider.ValueChanged -= eh)
                .Select(g => g.EventArgs.NewValue)
                .StartWith(GreenSlider.Value);

            var blueValue = Observable.FromEventPattern<EventHandler<ValueChangedEventArgs>, ValueChangedEventArgs>(eh => BlueSlider.ValueChanged += eh, eh => BlueSlider.ValueChanged -= eh)
                .Select(b => b.EventArgs.NewValue)
                .StartWith(BlueSlider.Value);

            var rgb = redValue.CombineLatest(greenValue, blueValue, (r, g, b) => new Tuple<double, double, double>(r, g, b));
            
            rgb.ObserveOn(SynchronizationContext.Current)
                .Subscribe(x =>
                {
                    ResultText.Text = x.ToString();
                    BoxView.Color = Color.FromRgb(x.Item1, x.Item2, x.Item3);
                });
        }
    }
}
