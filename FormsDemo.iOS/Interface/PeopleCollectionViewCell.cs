using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace FormsDemo.iOS.Interface
{
    public class PeopleCollectionViewCell : UICollectionViewCell
    {
        
        [Export("initWithFrame:")]
        public PeopleCollectionViewCell(CGRect frame) : base(frame)
        {
            var imageView = new UIImageView();
            var label = new UILabel();

            ContentView.AddSubview(imageView);
            ContentView.AddSubview(label);

            ContentView.AddConstraints(new NSLayoutConstraint[]
            {
                NSLayoutConstraint.Create(label, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(label, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, 0),
                NSLayoutConstraint.Create(label, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, 0),
                NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, 0),
                NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, 0),
                NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, 0),
                NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, label, NSLayoutAttribute.Top, 1, 0),
            });

        }

        public UIImageView ImageView { get; }
        public UILabel Label { get; }
    }
}