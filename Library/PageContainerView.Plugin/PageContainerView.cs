using System;
using Xamarin.Forms;

namespace Plugin.PCV
{
	/// <summary>
	/// A View that contains a Page
	/// </summary>
	public class PageContainerView : View
	{
		public PageContainerView() { }

		public static readonly BindableProperty ContentProperty = BindableProperty.Create<PageContainerView, Page>(s => s.Content, null);

		public Page Content
		{
			get { return (Page)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
	}
}
