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
	[Register ("SummaryViewController")]
	partial class SummaryViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_LeastTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_MostTimes { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_SummaryLeast { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_SummaryMost { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (t_LeastTime != null) {
				t_LeastTime.Dispose ();
				t_LeastTime = null;
			}
			if (t_MostTimes != null) {
				t_MostTimes.Dispose ();
				t_MostTimes = null;
			}
			if (t_SummaryLeast != null) {
				t_SummaryLeast.Dispose ();
				t_SummaryLeast = null;
			}
			if (t_SummaryMost != null) {
				t_SummaryMost.Dispose ();
				t_SummaryMost = null;
			}
		}
	}
}
