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
	[Register ("OptionsViewController")]
	partial class OptionsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Clear { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_RemoveLastDataPoint { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_TimeMax { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_TimeMin { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (b_Clear != null) {
				b_Clear.Dispose ();
				b_Clear = null;
			}
			if (b_RemoveLastDataPoint != null) {
				b_RemoveLastDataPoint.Dispose ();
				b_RemoveLastDataPoint = null;
			}
			if (b_TimeMax != null) {
				b_TimeMax.Dispose ();
				b_TimeMax = null;
			}
			if (b_TimeMin != null) {
				b_TimeMin.Dispose ();
				b_TimeMin = null;
			}
		}
	}
}
