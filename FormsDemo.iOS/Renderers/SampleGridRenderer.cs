using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using FFImageLoading;
using FFImageLoading.Transformations;
using FormsDemo.Common.Views;
using FormsDemo.iOS.Interface;
using FormsDemo.iOS.Renderers;
using FormsDemo.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(SampleGridView), typeof(SampleGridRenderer))]
namespace FormsDemo.iOS.Renderers
{
    public class SampleGridRenderer : ViewRenderer<SampleGridView, UICollectionView>
    {
        private PeopleDataSource _peopleDataSource;
        public static string PersonCellIdentifier = "PersonListCell";
        protected override void OnElementChanged(ElementChangedEventArgs<SampleGridView> e)
        {
            if (Control == null)
            {
                UICollectionView uiCollectionView = CreateNativeControl();
                SetNativeControl(uiCollectionView);
            }

        }

        private UICollectionView CreateNativeControl()
        {
            var uiCollectionView = new UICollectionView(new CGRect(0, 0, 100, 100), new UICollectionViewFlowLayout());
            _peopleDataSource = _peopleDataSource ?? new PeopleDataSource();
            uiCollectionView.Source = _peopleDataSource;
            uiCollectionView.RegisterClassForCell(typeof(PeopleCollectionViewCell), PersonCellIdentifier);
            return uiCollectionView;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "ItemSource")
            {
                _peopleDataSource.UpdateData(Element.ItemSource);
                Control?.ReloadData();
            }
        }
    }

    public class PeopleDataSource : UICollectionViewSource
    {
        public ObservableCollection<Person> People { get; private set; }

        public void UpdateData(ObservableCollection<Person> dataSet)
        {
            People = dataSet;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            PeopleCollectionViewCell cell = (PeopleCollectionViewCell)collectionView.DequeueReusableCell(SampleGridRenderer.PersonCellIdentifier, indexPath);

            var person = People[indexPath.Row];
            cell.Label.Text = $"{person.Name.First} {person.Name.Last}";
            ImageService.Instance.LoadUrl(person.Picture.Thumbnail)
                        .Transform(new CircleTransformation())
                        .IntoAsync(cell.ImageView);
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return People?.Count ?? 0;
        }
    }
}