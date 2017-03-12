using Xamarin.Forms;

namespace PCVExample
{
	public partial class PCVExamplePage : ContentPage
	{
		NavigationPage navPageA;
		NavigationPage navPageB;
		
		public PCVExamplePage()
		{
			InitializeComponent();

			CreateNavPages();
		}

		public void CreateNavPages()
		{
			var a1 = BuildChildPage("A", 1, Color.Green);
			navPageA = new NavigationPage(a1); //{ Title = "Nav Page A" }; // this Title doesn't seem to get displayed anywhere

			var b1 = BuildChildPage("B", 1, Color.Red);
			navPageB = new NavigationPage(b1);


			// set the content property of the PageContainer to display Nav Page A first
			pageContainerView.Content = navPageB;
		}


		ContentPage BuildChildPage(string parent, int childNumber, Color background)
		{
			var label = new Label { Text = $"Child Page {parent.ToLower()}.{childNumber}", VerticalOptions = LayoutOptions.Center };
			var button = new Button { Text = "Push next ContentPage", VerticalOptions = LayoutOptions.Center };

			button.Clicked += (sender, e) =>
			{
				// TODO: give child pages random colors
				var nextChildPage = BuildChildPage(parent, childNumber + 1, Color.Yellow);

				// use the correct navigation page
				if (parent == "A")
					navPageA.PushAsync(nextChildPage);
				else if (parent == "B")
					navPageB.PushAsync(nextChildPage);
			};

			var childPage = new ContentPage
			{
				Title = $"Nav Page {parent}",
				BackgroundColor = background,
				Content = new StackLayout
				{
					Children = { label, button }
				}
			};

			return childPage;
		}
	}
}
