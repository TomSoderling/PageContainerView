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

			// this 64px margin is a hack for iOS for now - it doesn't look right on iPhone in landscape orientation
			tabStack.Margin = new Thickness(0, 64, 0, 0);

			CreateNavPages();
		}

		public void CreateNavPages()
		{
			var a1 = BuildDynamicChildPage("A", 1, Color.Lime);
			navPageA = new NavigationPage(a1); //{ Title = "Nav Page A" }; // this Title doesn't seem to get displayed anywhere

			var b1 = BuildDynamicChildPage("B", 1, Color.Yellow);
			navPageB = new NavigationPage(b1);

			var c1 = BuildDynamicChildPage("C", 1, Color.Pink);
			navPageC = new NavigationPage(c1);


			// set the content property of the PageContainer to display Nav Page A first
			pageContainerView.Content = navPageA;
		}


		private ContentPage BuildDynamicChildPage(string parent, int childNumber, Color background)
		{
			var label = new Label 
			{ 
				Text = $"Child Page {parent.ToLower()}.{childNumber}", 
				Margin = new Thickness(15, 15, 0, 0) 
			};

			var button = new Button 
			{ 
				Text = "Push next child ContentPage", 
				HorizontalOptions = LayoutOptions.Start, 
				Margin = new Thickness(15, 0, 0, 0) 
			};

			button.Clicked += (sender, e) =>
			{
				var nextChildPage = BuildDynamicChildPage(parent, childNumber + 1, GetRandomColor());

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
