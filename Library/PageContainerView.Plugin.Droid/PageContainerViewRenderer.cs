using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Plugin.PCV.Droid;
using Android.Views;

[assembly: ExportRenderer(typeof(Plugin.PCV.PageContainerView), typeof(PageContainerViewRenderer))]
namespace Plugin.PCV.Droid
{
	public class PageContainerViewRenderer : ViewRenderer<PageContainerView, Android.Views.View>
	{
		private Page _currentPage;
		private bool _contentNeedsLayout;

		// We must have this method or else the Xamarin build system will remove code that it thinks is unused
		// https://forums.xamarin.com/discussion/comment/198852/%23Comment_198852
		public static void Init() { }

		protected override void OnElementChanged(ElementChangedEventArgs<PageContainerView> e)
		{
			System.Diagnostics.Debug.WriteLine($"PageViewContainerRenderer.OnElementChanged() - Droid");

			base.OnElementChanged(e);
			var pageViewContainer = e.NewElement as PageContainerView;

			if (e.NewElement != null)
			{
				DisplayPCVContent(e.NewElement.Content);
			}
			else
			{
				// TODO: Tom 3/22/17 - why would we want to call this?
				DisplayPCVContent(null);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($"PageViewContainerRenderer.OnElementPropertyChanged() - Droid - e.PropertyName = { e.PropertyName }");

			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "Content")
				DisplayPCVContent(Element.Content);
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			System.Diagnostics.Debug.WriteLine($"PageViewContainerRenderer.OnLayout() - Droid = { l } { t } { r } { b }");

			base.OnLayout(changed, l, t, r, b);

			if ((changed || _contentNeedsLayout) && this.Control != null)
			{
				if (_currentPage != null)
				{
					System.Diagnostics.Debug.WriteLine($"PageViewContainerRenderer.OnLayout() - Droid - Element.Width = { Element.Width } Element.Height = { Element.Height }");
					_currentPage.Layout(new Rectangle(0, 0, Element.Width, Element.Height));
				}

				var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
				var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

				System.Diagnostics.Debug.WriteLine($"PageViewContainerRenderer.OnLayout() - Droid - msw = { msw } msh = { msh }");

				this.Control.Measure(msw, msh);
				this.Control.Layout(0, 0, r, b);
				_contentNeedsLayout = false;

				OnLayout(false, l, t, r, b);
			}
		}

		private void DisplayPCVContent(Page pageToDisplay)
		{
			System.Diagnostics.Debug.WriteLine($"PageViewContainerRenderer.ChangePage() - Droid");

			//TODO handle current page
			if (pageToDisplay != null)
			{
				var parentPage = Element.GetParentPage();
				pageToDisplay.Parent = parentPage;

				//var existingRenderer = page.GetRenderer(); // old hacky way
				var existingRenderer = Platform.GetRenderer(pageToDisplay); // TODO: this is always null. The new page hasn't been rendered yet.

				if (existingRenderer == null)
				{
					var renderer = Platform.CreateRenderer(pageToDisplay);
					//page.SetRenderer(renderer); // old hacky way
					Platform.SetRenderer(pageToDisplay, renderer);

					//existingRenderer = page.GetRenderer(); // old hacky way
					existingRenderer = Platform.GetRenderer(pageToDisplay);
				}

				_contentNeedsLayout = true;
				SetNativeControl(existingRenderer.ViewGroup);
				Invalidate();
				//TODO update the page
				_currentPage = pageToDisplay;
			}
			else
			{
				//TODO - update the page
				_currentPage = null;
			}

			if (_currentPage == null)
			{
				//have to set somethign for android not to get pissy
				var view = new Android.Views.View(this.Context);
				view.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
				SetNativeControl(view);
			}
		}
	}
}