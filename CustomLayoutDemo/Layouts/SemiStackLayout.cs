using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomLayoutDemo.Layouts
{
    public class LayoutInformation
    {
        public List<Tuple<View, Rectangle>> ViewLayout { get; set; }
        public SizeRequest SizeRequest { get; set; }
    }

    public class SemiStackLayout : Layout<View>
    {
        public static BindableProperty TopViewProperty = BindableProperty.Create(nameof(TopView), typeof(View), typeof(SemiStackLayout), propertyChanged:ViewPropertyChanged);
        public static BindableProperty MiddleViewProperty = BindableProperty.Create(nameof(MiddleView), typeof(View), typeof(SemiStackLayout), propertyChanged: ViewPropertyChanged);
        public static BindableProperty BottomViewProperty = BindableProperty.Create(nameof(BottomView), typeof(View), typeof(SemiStackLayout), propertyChanged: ViewPropertyChanged);
        public static BindableProperty LowerRightViewProperty = BindableProperty.Create(nameof(TopRightView), typeof(View), typeof(SemiStackLayout), propertyChanged: ViewPropertyChanged);

        private LayoutInformation _layoutInformation;

        public View TopView
        {
            get { return (View) GetValue(TopViewProperty); }
            set { SetValue(TopViewProperty, value);}
        }

        public View MiddleView
        {
            get { return (View)GetValue(MiddleViewProperty); }
            set { SetValue(MiddleViewProperty, value); }
        }

        public View BottomView
        {
            get { return (View)GetValue(BottomViewProperty); }
            set { SetValue(BottomViewProperty, value); }
        }

        public View TopRightView
        {
            get { return (View)GetValue(LowerRightViewProperty); }
            set { SetValue(LowerRightViewProperty, value); }

        }

        public List<Tuple<View, Rectangle>> NaiveLayout(double width, double height)
        {
            List<Tuple<View, Rectangle>> layout = new List<Tuple<View, Rectangle>>();
            List<View> stackedViews = new List<View>() {TopView, MiddleView, BottomView};


            double previousY = 0;
            foreach (var view in stackedViews)
            {
                var measure = view.Measure(width, height, MeasureFlags.IncludeMargins);
                double x = 0;
                double requestedWidth = width;
                if (!view.HorizontalOptions.Expands && view.HorizontalOptions.Alignment != LayoutAlignment.Fill)
                {
                    requestedWidth = measure.Request.Width;
                    switch (view.HorizontalOptions.Alignment)
                    {
                        case LayoutAlignment.Center:
                            x = (width*0.5) - (requestedWidth*0.5);
                            break;
                        case LayoutAlignment.End:
                            x = width - requestedWidth;
                            break;
                        case LayoutAlignment.Start:
                        case LayoutAlignment.Fill:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                

                var rectangle = new Rectangle(x, previousY, requestedWidth, measure.Request.Height);
                layout.Add(new Tuple<View, Rectangle>(view, rectangle));

                previousY = rectangle.Bottom;
            }
            var topRightView = TopRightView;
            var size = topRightView.Measure(width, height, MeasureFlags.IncludeMargins);
            var topRightRectangle = new Rectangle(width - size.Request.Width, 0, size.Request.Width, size.Request.Height);
            layout.Add(new Tuple<View, Rectangle>(topRightView, topRightRectangle));

            return layout;
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (_layoutInformation == null)
            {
                _layoutInformation = new LayoutInformation();
                var layout = NaiveLayout(width, height);
                _layoutInformation.ViewLayout = layout;
            }
            foreach (Tuple<View, Rectangle>	viewRecTuple in _layoutInformation.ViewLayout)
            {
                LayoutChildIntoBoundingRegion(viewRecTuple.Item1, viewRecTuple.Item2);
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            Rectangle bounds = new Rectangle();

            if (_layoutInformation == null)
            {
                _layoutInformation = new LayoutInformation();
                var layout = NaiveLayout(widthConstraint, heightConstraint);
                _layoutInformation.ViewLayout = layout;
            }

            foreach (var tuple in _layoutInformation.ViewLayout)
            {
                bounds = Rectangle.Union(bounds, tuple.Item2);
            }

            return new SizeRequest(bounds.Size);
        }

        protected override void OnChildMeasureInvalidated()
        {
            base.OnChildMeasureInvalidated();
            _layoutInformation = null;
        }

        private static void ViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (SemiStackLayout) bindable;
            var oldView = (View) oldValue;
            var newView = (View) newValue;

            if (oldView != null)
                layout.Children.Remove(oldView);
            if (newView != null)
                layout.Children.Add(newView);
        }
    }
}
