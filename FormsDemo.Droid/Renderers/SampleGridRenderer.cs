using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Transformations;
using FFImageLoading.Views;
using FormsDemo.Common.Views;
using FormsDemo.Droid.Renderers;
using FormsDemo.Services;
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

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "ItemSource")
            {
                _adapter.UpdateDataSet(Element.ItemSource);
            }
        }

        protected override RecyclerView CreateNativeControl()
        {
            _recyclerView = new RecyclerView(Context);

            _recyclerView.HasFixedSize = true;

            //LinearLayoutManager layoutManager = new LinearLayoutManager(Context);
            GridLayoutManager layoutManager = new GridLayoutManager(Context, 3);
            _recyclerView.SetLayoutManager(layoutManager);

            _adapter = new ReyclerAdapter();
            _recyclerView.SetAdapter(_adapter);
            return _recyclerView;
        }


        public class ReyclerAdapter : RecyclerView.Adapter
        {
            public class ViewHolder : RecyclerView.ViewHolder
            {
                public ViewHolder(View itemView) : base(itemView)
                {
                    TextView = itemView.FindViewById<TextView>(Resource.Id.textView);
                    ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView);
                }

                public TextView TextView { get; }
                public ImageViewAsync ImageView { get; }
            }

            private ObservableCollection<Person> _dataSet;

            public ReyclerAdapter(ObservableCollection<Person> dataSet)
            {
                _dataSet = dataSet;
            }

            public ReyclerAdapter()
            {
                _dataSet = new ObservableCollection<Person>();
            }

            public void UpdateDataSet(ObservableCollection<Person> dataset)
            {
                _dataSet = dataset;
                NotifyDataSetChanged();
                
            }
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var viewHolder = (ViewHolder)holder;

                var person = _dataSet[position];

                viewHolder?.TextView.SetText($"{person.Name.First} {person.Name.Last}", TextView.BufferType.Normal);
                if (viewHolder != null)
                    ImageService.Instance.LoadUrl(person.Picture.Thumbnail)
                        .Transform(new CircleTransformation())
                        .IntoAsync(viewHolder.ImageView);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int viewType)
            {
                View view = LayoutInflater.From(viewGroup.Context)
                    .Inflate(Resource.Layout.textRowItem, viewGroup, false);

                ViewHolder viewHolder = new ViewHolder(view);
                return viewHolder;
            }

            public override int ItemCount => _dataSet.Count;
        }
    }
}