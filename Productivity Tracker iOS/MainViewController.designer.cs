// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Productivity_Tracker_iOS
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Awesome { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Good { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Mediocre { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Poor { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Terrible { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (b_Awesome != null) {
				b_Awesome.Dispose ();
				b_Awesome = null;
			}
			if (b_Good != null) {
				b_Good.Dispose ();
				b_Good = null;
			}
			if (b_Mediocre != null) {
				b_Mediocre.Dispose ();
				b_Mediocre = null;
			}
			if (b_Poor != null) {
				b_Poor.Dispose ();
				b_Poor = null;
			}
			if (b_Terrible != null) {
				b_Terrible.Dispose ();
				b_Terrible = null;
			}
		}
	}
}
