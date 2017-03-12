using System;
using Xamarin.Forms;

namespace Plugin.PCV
{
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
