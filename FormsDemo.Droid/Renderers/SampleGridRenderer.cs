using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FormsDemo.Common.Views;
using Xamarin.Forms.Platform.Android.AppCompat;

namespace FormsDemo.Droid.Renderers
{
    class SampleGridRenderer : ViewRenderer<SampleGridView, RecyclerView>
    {
        protected override RecyclerView CreateNativeControl()
        {

            throw new NotImplementedException();
        }
    }
}