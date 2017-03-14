using System;
using System.Diagnostics;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Plugin.PCV.iOS
{
	public class ViewControllerContainer : UIView
	{
		public ViewControllerContainer(CGRect frame) : base(frame)
		{
			BackgroundColor = Color.Transparent.ToUIColor();
		}

		#region properties

		public UIViewController ParentViewController { get; set; }

		private UIViewController _viewController;
		public UIViewController ViewController
		{
			get { return _viewController; }
			set
			{
				if (_viewController != null)
				{
					RemoveCurrentViewController();
				}
				_viewController = value;

				if (_viewController != null)
				{
					AddViewController();
				}
			}
		}

		private void AddViewController()
		{
			if (ParentViewController == null)
				throw new Exception("No Parent ViewController was found");

			Debug.WriteLine($"vc.v is: {_viewController.View}");


			// This is the new ViewController of the new Page to be displayed - give it a parent.
			ParentViewController.AddChildViewController(_viewController); 

			// add this new View as a nested view of this current UIVew
			AddSubview(_viewController.View); 

			_viewController.View.Frame = Bounds;
			_viewController.DidMoveToParentViewController(ParentViewController);

		}

		private void RemoveCurrentViewController()
		{
			if (ViewController != null)
			{
				ViewController.WillMoveToParentViewController(null);
				ViewController.View.RemoveFromSuperview();
				ViewController.RemoveFromParentViewController();
			}
		}

		#endregion

		#region lifecycle

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			//hack to fix sizing of children when changing orientation
			if (ViewController != null && ViewController.View.Subviews.Length > 0)
			{
				foreach (UIView view in ViewController.View.Subviews)
					view.Frame = Bounds;
			}
			//			if (ViewController != null) {
			//				ViewController.View.Frame = Bounds;
			//			}
		}

		#endregion

	}
}
