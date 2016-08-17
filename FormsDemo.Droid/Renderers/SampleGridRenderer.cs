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
using FormsDemo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly:ExportRenderer(typeof(SampleGridView), typeof(SampleGridRenderer))]
namespace FormsDemo.Droid.Renderers
{
    public class SampleGridRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<SampleGridView, RecyclerView>
    {
        private RecyclerView _recyclerView;
        private ReyclerAdapter _adapter;
        protected override void OnElementChanged(ElementChangedEventArgs<SampleGridView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                RecyclerView recyclerView = CreateNativeControl();
                SetNativeControl(recyclerView);
            }
        }

        protected override RecyclerView CreateNativeControl()
        {
            _recyclerView = new RecyclerView(Context);

            _recyclerView.HasFixedSize = true;

            //LinearLayoutManager layoutManager = new LinearLayoutManager(Context);
            GridLayoutManager layoutManager = new GridLayoutManager(Context, 2);
            _recyclerView.SetLayoutManager(layoutManager);

            _adapter = new ReyclerAdapter(GenerateDataSet());
            _recyclerView.SetAdapter(_adapter);
            return _recyclerView;
        }

        public string[] GenerateDataSet()
        {
            var data = new string[100000];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = $"Data_{i}";
            }
            return data;
        }

        public class ReyclerAdapter : RecyclerView.Adapter
        {
            private readonly string[] _dataSet;

            public ReyclerAdapter(string[] dataSet)
            {
                _dataSet = dataSet;
            }

            public class ViewHolder : RecyclerView.ViewHolder
            {
                public ViewHolder(View itemView) : base(itemView)
                {
                    TextView = itemView.FindViewById<TextView>(Resource.Id.textView);
                }

                public TextView TextView { get; }
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var viewHolder = (ViewHolder)holder;

                viewHolder?.TextView.SetText(_dataSet[position], TextView.BufferType.Normal);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int viewType)
            {
                View view = LayoutInflater.From(viewGroup.Context)
                    .Inflate(Resource.Layout.textRowItem, viewGroup, false);

                ViewHolder viewHolder = new ViewHolder(view);
                return viewHolder;
            }

            public override int ItemCount => _dataSet.Length;
        }
    }
}