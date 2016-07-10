using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Xamarin.Forms;

namespace Productivity_Tracker_iOS
{
    partial class OptionsViewController : UIViewController
    {
        public OptionsViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            b_TimeMin.TouchUpInside += TimeMinClicked;
            b_TimeMax.TouchUpInside += TimeMaxClicked;
            b_Save.TouchUpInside += SaveClicked;

            b_RemoveLastDataPoint.TouchUpInside += RemoveLastPointClicked;
            b_Clear.TouchUpInside += ClearClicked;

            v_TimePicker.Hidden = true;
            b_Save.Hidden = true;

            b_Clear.Hidden = false;
            b_RemoveLastDataPoint.Hidden = false;
            t_Clear.Hidden = false;
            t_RemoveDataPoint.Hidden = false;
            t_TimeMin.Text = ConvertTime("Start Time: ", AppDelegate.hourMin, AppDelegate.minuteMin);
            t_TimeMax.Text = ConvertTime("End Time: ", AppDelegate.hourMax, AppDelegate.minuteMax);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            //database is empty, disable buttons
            if (AppDelegate.db.Table<ProductiveData>().Count() == 0)
            {
                DisableButton(b_Clear);
                DisableButton(b_RemoveLastDataPoint);
            }
            else
            {
                EnableButton(b_Clear);
                EnableButton(b_RemoveLastDataPoint);
            }
        }

        void SaveClicked(object sender, EventArgs e)
        {
            int HourMin = 8, MinuteMin = 20, HourMax = 0, MinuteMax = 0;
            bool saveTimes = false;
            if (t_DataTitle.Text.Equals("Earliest Notification Time"))
            {
                HourMin = NSDateToDateTime(v_TimePicker.Date).Hour + 1;
                MinuteMin = NSDateToDateTime(v_TimePicker.Date).Minute;
                if(HourMin < AppDelegate.hourMax)
                {
                    saveTimes = true;
                    AppDelegate.hourMin = HourMin;
                    AppDelegate.minuteMin = MinuteMin;
                }
            }
            else //Latest Notification Time
            {
                HourMax = NSDateToDateTime(v_TimePicker.Date).Hour + 1;
                MinuteMax = NSDateToDateTime(v_TimePicker.Date).Minute;
                if (AppDelegate.hourMin < HourMax)
                {
                    saveTimes = true;
                    AppDelegate.hourMax = HourMax;
                    AppDelegate.minuteMax = MinuteMax;
                }
            }

            if (saveTimes)
            {
                v_TimePicker.Hidden = true;
                b_Save.Hidden = true;

                b_Clear.Hidden = false;
                b_RemoveLastDataPoint.Hidden = false;
                EnableButton(b_TimeMin);
                EnableButton(b_TimeMax);
                t_Clear.Hidden = false;
                t_RemoveDataPoint.Hidden = false;
                t_DataTitle.Text = "Data";
                t_TimeMin.Text = ConvertTime("Start Time: ", AppDelegate.hourMin, AppDelegate.minuteMin);
                t_TimeMax.Text = ConvertTime("End Time: ", AppDelegate.hourMax, AppDelegate.minuteMax);
            }
            else
            {
                t_DataTitle.Text = "Error: Time Mismatch";
            }
        }

        void TimeMinClicked(object sender, EventArgs e)
        {
            v_TimePicker.Date = DateTimeToNSDate(DateTime.Today.AddHours(AppDelegate.hourMin - 1).AddMinutes(AppDelegate.minuteMin));
            v_TimePicker.Hidden = false;
            b_Save.Hidden = false;

            b_Clear.Hidden = true;
            b_RemoveLastDataPoint.Hidden = true;
            DisableButton(b_TimeMin);
            DisableButton(b_TimeMax);
            t_Clear.Hidden = true;
            t_RemoveDataPoint.Hidden = true;
            t_DataTitle.Text = "Earliest Notification Time";
        }

        void TimeMaxClicked(object sender, EventArgs e)
        {
            v_TimePicker.Date = DateTimeToNSDate(DateTime.Today.AddHours(AppDelegate.hourMax - 1).AddMinutes(AppDelegate.minuteMax));
            v_TimePicker.Hidden = false;
            b_Save.Hidden = false;

            b_Clear.Hidden = true;
            b_RemoveLastDataPoint.Hidden = true;
            DisableButton(b_TimeMin);
            DisableButton(b_TimeMax);
            t_Clear.Hidden = true;
            t_RemoveDataPoint.Hidden = true;
            t_DataTitle.Text = "Latest Notification Time";
        }

        void RemoveLastPointClicked(object sender, EventArgs e)
        {
            var lastData = new ProductiveData();

            //find last data point
            foreach (var dataPoint in AppDelegate.db.Table<ProductiveData>())
            {
                lastData = dataPoint;
            }

            AppDelegate.db.Delete<ProductiveData>(lastData.DataNum);
            //database is empty
            if (AppDelegate.db.Table<ProductiveData>().Count() == 0)
            {
                DisableButton(b_Clear);
                DisableButton(b_RemoveLastDataPoint);
            }
        }

        void ClearClicked(object sender, EventArgs e)
        {
            AppDelegate.db.DeleteAll<ProductiveData>();
            b_Clear.Enabled = false;
            DisableButton(b_Clear);
            DisableButton(b_RemoveLastDataPoint);
        }

        public void EnableButton(UIButton button)
        {
            button.Enabled = true;
            button.SetTitleColor(UIColor.FromRGB(246, 246, 246), UIControlState.Normal);
            button.BackgroundColor = UIColor.FromRGB(41, 128, 185);
        }

        void DisableButton(UIButton button)
        {
            button.Enabled = false;
            button.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
            button.BackgroundColor = UIColor.FromRGB(43, 43, 43);
        }

        //Conert time from 24 hour clock to 12 hour clock
        string ConvertTime(string openingStatement, int hour, int min)
        {
            bool isPM = false;

            if (hour > 12 && hour != 24)
            {
                isPM = true;
                hour -= 12;
            }
            else if (hour == 0 || hour == 24)
            {
                hour = 12;
                isPM = false;
            }
            else if (hour == 12)
            {
                isPM = true;
            }

            if (isPM)
            {
                return string.Format(openingStatement + "{0}:{1}PM", hour, min.ToString().PadLeft(2, '0'));
            }
            return string.Format(openingStatement + "{0}:{1}AM", hour, min.ToString().PadLeft(2, '0'));
        }

        //conversion methods - Source: http://sourcerer.tumblr.com/post/502919332/nsdate-to-datetime-and-back
        public static DateTime NSDateToDateTime(NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        public static NSDate DateTimeToNSDate(DateTime date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - reference).TotalSeconds);
        }
    }
}
