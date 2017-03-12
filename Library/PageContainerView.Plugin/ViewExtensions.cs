using System;
using Xamarin.Forms;

namespace Plugin.PCV
{
	public static class ViewExtensions
	{
		/// <summary>
		/// Gets the page parent that the element belongs to
		/// </summary>
		/// <returns>The page parent.</returns>
		/// <param name="element">Element.</param>
		public static Page GetParentPage(this VisualElement element)
		{
			if (element != null)
			{
				var parent = element.Parent;
				while (parent != null)
				{
					if (parent is Page)
						return parent as Page;
					
					parent = parent.Parent; // keep walking up the visual tree
				}
			}

			return null;
		}
	}
}
