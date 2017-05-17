## The Problem

[Page](https://developer.xamarin.com/api/type/Xamarin.Forms.Page) is probably one of the most foundational and essential Views (aka controls) in all of Xamarin.Forms.  Being a top level UI element, nearly everything is built on top of a derived class of Page. By definition, Page is a VisualElement that occupies the entire screen, and that use certainly makes sense.  

The trouble comes when you want to create a more "interesting" main app layout or use a different navigation pattern.  This need came to light as we tried to make an app that looks like one of the Microsoft Office apps for iOS.  They feature a set of navigation tabs on the left side, with the main content on the right taking up most of the screen.  Notice, this is different than a master-detail layout.  

Here is what the PowerPoint and Excel apps look like:

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
In Xamarin.Forms, the UI design is actually an architectural decision.  In order to achive what I consider to be a pretty basic app layout we had to take a "composite approach", where the entire app was made up of ContentViews (as opposed to ContentPages as one usually would use).  There was only 1 single ContentPage in the entire app, and it housed the nested ContentView layout.  The tabs on the left were inside a ContentView, and the entire right area was also a ContentView that would get swapped out with other ContentViews that contained our UI when a tab was tapped, or when we needed to navigation further down the path on a particular tab.  

While it helped us achieve the look we wanted, this composite approach ended up sucking for us for several reasons. These are some that I can remember:
 - we missed using NavigationPages and having the navigation stack managed behind the scenes
 - we really missed all the lifecycle events that ContentPage provides to override: OnAppearing, OnPropertyChanged, OnBackButtonPressed, etc.
 - there were no page transitions when navigating anywhere, which gave the app a weird hybrid-app feel
 - the composite approach requred a LOT of nesting of ContentViews resulting in complexity and a performance hit in some cases
 - all the business logic lived in the ContentViews, so instead of being reusable custom controls like you'd normal have, we had tightly coupled ContentViews that represented a single "page".
 
What if we could just drop a NavigationPage on the right side and be happily on our way?  Better yet, what if we could simply place a Page anywhere you wanted in a Xamarin.Forms app, just like we do with Views?  


## An Answer

This library is built around that single concept.  It introduces a new type of View (control) called PageContainerView.  It's a View that acts as a container for a Page, and allows you to place a ContentPage or NavigationPage anywhere in the app that you can place a View.  
How do you get a Page to appear in place of a View?  All the magic happens in a custom renderer for each platform: iOS, Android, and UWP (work in progress).


## The Inspiration

Like most interesting ideas, someone else has also thought of it.  The developers at [Twin Technologies](http://twintechs.com) saw the need for something like this and created a brilliant library called [TwinTechsFormsLib](https://github.com/twintechs/TwinTechsFormsLib) to push the boundaries of what Xamarin.Forms was capable of at that time.  Notice that their readme file doesn't mention the View (they called it PageViewContainer), but this blog post ???? does.  
This library is a fork of their initial prototype and they deserve most of the credit for anything this library can do.  My contribution was to fix their first version of the View  by replacing some of the hacky bits that they had to use at the time, with functionality that Xamarin.Forms now supports, add support for UWP, and extract the parts related to the PageContainerView into a more focussed library.  


## How To Use This Thing (currently)


## Known Limitations

- On iOS, after rotating the device from portrait to landscape, then back to portrait, you'll notice that the right side of the Page gets chopped off.  It's not correcting the width of the page after rotation.


## Please Help

I would really appreciate your feedback, particularly in answering these few questions:  
1. Does the description make sense? Do you get the idea?
   - I’m already familiar with the concept, so does the description help you grasp the concept?
   - Does the current name make sense?
2. Do you find this View to be useful or even necessary?  Would you use it?
3. Do the directions make sense?
   - What barriers are there for you to try it out? How can I lower those?
