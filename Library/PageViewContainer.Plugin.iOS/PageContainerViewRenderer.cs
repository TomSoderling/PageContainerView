using System;
using Plugin.PCV;
using Plugin.PCV.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PageContainerView), typeof(PageContainerViewRenderer))]
namespace Plugin.PCV.iOS
{
	public class PageContainerViewRenderer : ViewRenderer<PageContainerView, ViewControllerContainer>
	{
		public PageContainerViewRenderer() { }

		/// <summary>
		/// This method needs to be called from the ExampleApp.iOS AppDelegate so this class doesn't get linked out
		/// This is needed since the renderer exists in a different namespace than the application
		/// </summary>
		public new static void Init()
		{
			var temp = DateTime.Now;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<PageContainerView> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.ViewController = null;
			}

			if (e.NewElement != null)
			{
				var viewControllerContainer = new ViewControllerContainer(Bounds);
				SetNativeControl(viewControllerContainer);
			}
		}

		void ChangePage(Page newPageToDisplay)
		{
			if (newPageToDisplay != null)
			{
				newPageToDisplay.Parent = Element.GetParentPage();

				//var pageRenderer = page.GetRenderer(); // old hacky way
				var pageRenderer = Platform.GetRenderer(newPageToDisplay); // TODO: this is always null. The new page hasn't been rendered yet.

				UIViewController viewController = null;
				if (pageRenderer != null && pageRenderer.ViewController != null)
					viewController = pageRenderer.ViewController;
				else
					viewController = newPageToDisplay.CreateViewController();

				var parentPage = Element.GetParentPage();
				//var renderer = parentPage.GetRenderer(); // old hacky way
				var parentPageRenderer = Platform.GetRenderer(parentPage);

				Control.ParentViewController = parentPageRenderer.ViewController;
				Control.ViewController = viewController;
			}
			else
			{
				if (Control != null)
				{
					Control.ViewController = null;
				}
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			var page = Element != null ? Element.Content : null;
			if (page != null)
			{
				page.Layout(new Rectangle(0, 0, Bounds.Width, Bounds.Height));
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
			{
				Device.BeginInvokeOnMainThread(() => ChangePage(Element != null ? Element.Content : null));
			}
		}

	}
}
