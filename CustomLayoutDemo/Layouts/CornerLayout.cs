using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomLayoutDemo.Layouts
{
    public class CornerLayout : Layout<View>
    {
        public static BindableProperty UpperLeftViewProperty = BindableProperty.Create(nameof(UpperLeftView), typeof(View), typeof(CornerLayout), propertyChanged:ViewPropertyChanged);
        public static BindableProperty UpperRightViewProperty = BindableProperty.Create(nameof(UpperRightView), typeof(View), typeof(CornerLayout), propertyChanged: ViewPropertyChanged);
        public static BindableProperty LowerLeftViewProperty = BindableProperty.Create(nameof(LowerLeftView), typeof(View), typeof(CornerLayout), propertyChanged: ViewPropertyChanged);
        public static BindableProperty LowerRightViewProperty = BindableProperty.Create(nameof(LowerRightView), typeof(View), typeof(CornerLayout), propertyChanged: ViewPropertyChanged);

        public View UpperLeftView
        {
            get { return (View) GetValue(UpperLeftViewProperty); }
            set { SetValue(UpperLeftViewProperty, value);}
        }

        public View UpperRightView
        {
            get { return (View)GetValue(UpperRightViewProperty); }
            set { SetValue(UpperRightViewProperty, value); }
        }

        public View LowerLeftView
        {
            get { return (View)GetValue(LowerLeftViewProperty); }
            set { SetValue(LowerLeftViewProperty, value); }
        }

        public View LowerRightView
        {
            get { return (View)GetValue(LowerRightViewProperty); }
            set { SetValue(LowerRightViewProperty, value); }

        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (UpperLeftView != null)
            {
                LayoutChildIntoBoundingRegion(UpperLeftView, new Rectangle(0, 0, 100, 100));
            }
            if (UpperRightView != null)
            {
                LayoutChildIntoBoundingRegion(UpperRightView, new Rectangle(width - 100, 0, 100, 100));
            }
            if (LowerLeftView != null)
            {
                LayoutChildIntoBoundingRegion(LowerLeftView, new Rectangle(0, height - 100, 100, 100));
            }
            if (LowerRightView != null)
            {
                var size = LowerRightView.Measure(100, 100, MeasureFlags.IncludeMargins);
                LayoutChildIntoBoundingRegion(LowerRightView, new Rectangle(width - size.Request.Width, height - size.Request.Height, size.Request.Width, size.Request.Height));
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, heightConstraint));
        }

        private static void ViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (CornerLayout) bindable;
            var oldView = (View) oldValue;
            var newView = (View) newValue;

            if (oldView != null)
                layout.Children.Remove(oldView);
            if (newView != null)
                layout.Children.Add(newView);
        }
    }
}
