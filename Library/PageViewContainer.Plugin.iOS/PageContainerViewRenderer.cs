﻿using System;
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

		// This method needs to be called from the ExampleApp.iOS AppDelegate so this class doesn't get linked out
		// https://forums.xamarin.com/discussion/comment/198852/#Comment_198852
		public new static void Init() { }

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
				Control.ViewController = viewController; // some logic happens here when this gets set

				LayoutSubviews(); // Need to adjust the layout after page change
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
				page.Layout(new Rectangle(0, 0, Bounds.Width, Bounds.Height));
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
			{
				if (Element?.Content != null) // don't attempt to change the page if the PCV.Content property doesn't have a page to display
				{
					// We must call this when Element.Content is a NavigationPage otherwise Platform.GetRenderer(parentPage) will return null in ChangePage()
					Device.BeginInvokeOnMainThread(() => ChangePage(Element.Content));
				}
			}
		}

	}
}
