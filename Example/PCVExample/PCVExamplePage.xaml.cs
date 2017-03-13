using System;
using Xamarin.Forms;

namespace PCVExample
{
	public partial class PCVExamplePage : ContentPage
	{
		NavigationPage navPageA;
		NavigationPage navPageB;
		NavigationPage navPageC;
		
		public PCVExamplePage()
		{
			InitializeComponent();

			CreateNavPages();
		}

		public void CreateNavPages()
		{
			var a1 = BuildChildPage("A", 1, Color.Lime);
			navPageA = new NavigationPage(a1); //{ Title = "Nav Page A" }; // this Title doesn't seem to get displayed anywhere

			var b1 = BuildChildPage("B", 1, Color.Yellow);
			navPageB = new NavigationPage(b1);

			var c1 = BuildChildPage("C", 1, Color.Pink);
			navPageC = new NavigationPage(c1);


			// set the content property of the PageContainer to display Nav Page A first
			pageContainerView.Content = navPageA;
		}


		private ContentPage BuildChildPage(string parent, int childNumber, Color background)
		{
			var label = new Label { Text = $"Child Page {parent.ToLower()}.{childNumber}" };
			var button = new Button { Text = "Push next ContentPage", HorizontalOptions = LayoutOptions.Start };

			button.Clicked += (sender, e) =>
			{
				var nextChildPage = BuildChildPage(parent, childNumber + 1, GetRandomColor());

				// use the correct navigation page
				switch (parent)
				{
					case "A":
						navPageA.PushAsync(nextChildPage);
						break;
					case "B":
						navPageB.PushAsync(nextChildPage);
						break;
					case "C":
						navPageC.PushAsync(nextChildPage);
						break;
					default:
						break;
				}
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

		private Color GetRandomColor()
		{
			var rand = new Random();
			return Color.FromRgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
		}



		void OnTabATapGestureRecognizerTapped(object sender, EventArgs args)
		{
			// switch to the NavigationPage for Tab A
			pageContainerView.Content = navPageA;
		}

		void OnTabBTapGestureRecognizerTapped(object sender, EventArgs args)
		{
			// switch to the NavigationPage for Tab B
			pageContainerView.Content = navPageB;
		}

		void OnTabCTapGestureRecognizerTapped(object sender, EventArgs args)
		{
			// switch to the NavigationPage for Tab C
			pageContainerView.Content = navPageC;
		}
	}
}
