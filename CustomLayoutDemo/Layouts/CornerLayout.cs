using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomLayoutDemo.Layouts
{
    public class SemiStackLayout : Layout<View>
    {
        public static BindableProperty TopViewProperty = BindableProperty.Create(nameof(TopView), typeof(View), typeof(SemiStackLayout), propertyChanged:ViewPropertyChanged);
        public static BindableProperty MiddleViewProperty = BindableProperty.Create(nameof(MiddleView), typeof(View), typeof(SemiStackLayout), propertyChanged: ViewPropertyChanged);
        public static BindableProperty BottomViewProperty = BindableProperty.Create(nameof(BottomView), typeof(View), typeof(SemiStackLayout), propertyChanged: ViewPropertyChanged);
        public static BindableProperty LowerRightViewProperty = BindableProperty.Create(nameof(LowerRightView), typeof(View), typeof(SemiStackLayout), propertyChanged: ViewPropertyChanged);

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

        public View LowerRightView
        {
            get { return (View)GetValue(LowerRightViewProperty); }
            set { SetValue(LowerRightViewProperty, value); }

        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (TopView != null)
            {
                LayoutChildIntoBoundingRegion(TopView, new Rectangle(0, 0, 100, 100));
            }
            if (MiddleView != null)
            {
                LayoutChildIntoBoundingRegion(MiddleView, new Rectangle(width - 100, 0, 100, 100));
            }
            if (BottomView != null)
            {
                LayoutChildIntoBoundingRegion(BottomView, new Rectangle(0, height - 100, 100, 100));
            }
            if (LowerRightView != null)
            {
                var size = LowerRightView.Measure(100, 100, MeasureFlags.IncludeMargins);
                LayoutChildIntoBoundingRegion(LowerRightView, new Rectangle(width - size.Request.Width, height - size.Request.Height, size.Request.Width, size.Request.Height));
            }
        }

        public Dictionary<View, Rectangle> NaiveLayout(double width, double height)
        {
            Dictionary<View, Rectangle> layout = new Dictionary<View, Rectangle>();

            if (TopView != null)
            {
                var measure = TopView.Measure(width, height, MeasureFlags.IncludeMargins);
                var rectangle = new Rectangle(0, 0, measure.Request.Width, measure.Request.Height);
                layout[TopView] = rectangle;
            }
            if (MiddleView != null)
            {
                LayoutChildIntoBoundingRegion(MiddleView, new Rectangle(width - 100, 0, 100, 100));
            }
            if (BottomView != null)
            {
                LayoutChildIntoBoundingRegion(BottomView, new Rectangle(0, height - 100, 100, 100));
            }
            if (LowerRightView != null)
            {
                var size = LowerRightView.Measure(100, 100, MeasureFlags.IncludeMargins);
                LayoutChildIntoBoundingRegion(LowerRightView, new Rectangle(width - size.Request.Width, height - size.Request.Height, size.Request.Width, size.Request.Height));
            }
            return layout;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, heightConstraint));
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
            layout.InvalidateMeasure();
        }
    }
}
