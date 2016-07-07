using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Xamarin.Forms;

namespace Productivity_Tracker_iOS
{
	partial class OptionsViewController : UIViewController
	{
		public OptionsViewController (IntPtr handle) : base (handle)
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
            t_DataTitle.Text = "Data";
        }

        void SaveClicked(object sender, EventArgs e)
        {
            v_TimePicker.Hidden = true;
            b_Save.Hidden = true;

            b_Clear.Hidden = false;
            b_RemoveLastDataPoint.Hidden = false;
            b_TimeMin.Enabled = true;
            b_TimeMax.Enabled = true;
            t_Clear.Hidden = false;
            t_RemoveDataPoint.Hidden = false;
            t_DataTitle.Text = "Data";
        }

        void TimeMinClicked (object sender, EventArgs e)
        {
            v_TimePicker.Hidden = false;
            b_Save.Hidden = false;

            b_Clear.Hidden = true;
            b_RemoveLastDataPoint.Hidden = true;
            b_TimeMin.Enabled = false;
            b_TimeMax.Enabled = false;
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
            b_TimeMin.Enabled = false;
            b_TimeMax.Enabled = false;
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
        }

        void ClearClicked(object sender, EventArgs e)
        {
            AppDelegate.db.DeleteAll<ProductiveData>();
            b_Clear.Enabled = false;
        }
    }
}
