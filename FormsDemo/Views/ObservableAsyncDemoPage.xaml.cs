using System;
using System.Reactive.Linq;
using System.Threading;
using FormsDemo.Services;
using Xamarin.Forms;

namespace FormsDemo.Views
{
    public partial class ObservableAsyncDemoPage : ContentPage
    {
        public ObservableAsyncDemoPage()
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

            var slowService = new SlowService();

            rgb.Throttle(TimeSpan.FromSeconds(0.25))
                .Select(t => Observable.FromAsync(token => slowService.Average(t.Item1, t.Item2, t.Item3, token)))
                .Switch()
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(x =>
                {
                    ResultText.Text = x.ToString();
                });
        }
    }
}
