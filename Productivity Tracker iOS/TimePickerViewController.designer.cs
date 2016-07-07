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
	[Register ("TimePickerViewController")]
	partial class TimePickerViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_Save { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIDatePicker v_DatePicker { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (b_Save != null) {
				b_Save.Dispose ();
				b_Save = null;
			}
			if (v_DatePicker != null) {
				v_DatePicker.Dispose ();
				v_DatePicker = null;
			}
		}
	}
}
