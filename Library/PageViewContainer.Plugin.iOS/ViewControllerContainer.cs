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


		public UIViewController ParentViewController { get; set; }

		// this prop handles the adding and removing of the ViewController to the Parent, and the View as a subview of this UIView
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


			// TODO: it still thinks the Bounds are 924 wide in portrait mode
			// it's not calculating this correctly after orentation change.  The height is getting updated correctly tho.

			// This is the new ViewController of the new Page to be displayed - give it a parent.
			ParentViewController.AddChildViewController(_viewController); 

			// add this new View as a nested view of this current UIVew
			AddSubview(_viewController.View);

			Debug.WriteLine(">>> Set _viewController.View.Frame to Bounds.");
			Debug.WriteLine($"                    Bounds: {Bounds}");
			Debug.WriteLine($"_viewController.View.Frame: {_viewController.View.Frame}");
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
	}
}
