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

            t_Clear.Editable = false;
            t_DataTitle.Editable = false;
            t_NotificationTitle.Editable = false;
            t_RemoveDataPoint.Editable = false;
            t_TimeMax.Editable = false;
            t_TimeMin.Editable = false;

            v_TimePicker.Hidden = true;
            b_Save.Hidden = true;

            b_Clear.Hidden = false;
            b_RemoveLastDataPoint.Hidden = false;
            t_Clear.Hidden = false;
            t_RemoveDataPoint.Hidden = false;
            t_DataTitle.Text = "Data";

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            //database is empty, disable buttons
            if (AppDelegate.db.Table<ProductiveData>().Count() == 0)
            {
                DisableButton(b_Clear);
                DisableButton(b_RemoveLastDataPoint);
            } else
            {
                EnableButton(b_Clear);
                EnableButton(b_RemoveLastDataPoint);
            }
        }

        void SaveClicked(object sender, EventArgs e)
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
        }

        void TimeMinClicked(object sender, EventArgs e)
        {
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
            if(AppDelegate.db.Table<ProductiveData>().Count() == 0)
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
    }
}
