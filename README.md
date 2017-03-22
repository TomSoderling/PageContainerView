## The Problem

[Page](https://developer.xamarin.com/api/type/Xamarin.Forms.Page) is probably one of the most foundational and essential Views (Controls) in all of Xamarin.Forms.  Being a top level UI element, nearly everything is built on top of a derived class of Page. By definition, Page is a VisualElement that occupies the entire screen, and that use certainly makes sense.  

The trouble comes when you want to create a more "interesting" main app layout or use a different navigation pattern.  This need came to light as we tried to make an app that looks like one of the Microsoft Office apps for iOS.  They feature a set of navigation tabs on the left side, with the main content on the right taking up most of the screen.  Notice, this is not a master-detail layout. Here is what PowerPoint and Excel look like:

<table>
 <tr>
  <td>
   <img src="/desc/PowerPoint_iOS.JPG" width="500"> 
  </td>
  <td>
   <img src="/desc/Excel_iOS.JPG" width="500">
  </td>
 </tr>
</table>

Yes, I know that tab navigation on the left side is non-standard for iOS or Android, but what if you have a good reason for it?  Seems reasonable that we could make this work, right?  Well, wrong (we found out).  
In Xamarin.Forms, the UI design is actually an architectural decision.  In order to achive this basic app layout we had to take what I'd call a "composite approach", where the entire app was made up of ContentViews (instead of ContentPages).  There was only 1 single ContentPage in the entire app, and it housed the nested ContentView layout.  The tabs on the left were inside a ContentView, and the entire right area was also a ContentView that would get swapped out with other ContentViews that contained our UI when a tab was tapped, or when we needed to navigation further down the path on a particular tab.  

This composite approach ended up sucking for us for several reasons. These are some that I can remember:
 - we missed using NavigationPages and having the navigation stack managed behind the scenes
 - we really missed all the lifecycle events that ContentPage provides to override: OnAppearing, OnPropertyChanged, OnBackButtonPressed, etc.
 - there were no page transitions when navigating anywhere, which gave the app a weird hybrid-app feel
 - the composite approach requred a LOT of nesting of ContentViews resulting in complexity and a performance hit in some cases
 
What if we could just place a NavigationPage on the right side, and be happily on our way?  Better yet, what if we could simply place a Page anywhere you wanted in a Xamarin.Forms app, just like we do with Views?  

## The Inspiration

