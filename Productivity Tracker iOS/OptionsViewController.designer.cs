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
		UIButton b_Save { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_TimeMax { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton b_TimeMin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_Clear { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_DataTitle { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_NotificationTitle { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_RemoveDataPoint { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_TimeMax { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView t_TimeMin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIDatePicker v_TimePicker { get; set; }

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
			if (b_Save != null) {
				b_Save.Dispose ();
				b_Save = null;
			}
			if (b_TimeMax != null) {
				b_TimeMax.Dispose ();
				b_TimeMax = null;
			}
			if (b_TimeMin != null) {
				b_TimeMin.Dispose ();
				b_TimeMin = null;
			}
			if (t_Clear != null) {
				t_Clear.Dispose ();
				t_Clear = null;
			}
			if (t_DataTitle != null) {
				t_DataTitle.Dispose ();
				t_DataTitle = null;
			}
			if (t_NotificationTitle != null) {
				t_NotificationTitle.Dispose ();
				t_NotificationTitle = null;
			}
			if (t_RemoveDataPoint != null) {
				t_RemoveDataPoint.Dispose ();
				t_RemoveDataPoint = null;
			}
			if (t_TimeMax != null) {
				t_TimeMax.Dispose ();
				t_TimeMax = null;
			}
			if (t_TimeMin != null) {
				t_TimeMin.Dispose ();
				t_TimeMin = null;
			}
			if (v_TimePicker != null) {
				v_TimePicker.Dispose ();
				v_TimePicker = null;
			}
		}
	}
}
