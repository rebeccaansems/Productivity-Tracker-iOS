using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

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

            b_RemoveLastDataPoint.TouchUpInside += RemoveLastPointClicked;
            b_Clear.TouchUpInside += ClearClicked;
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
