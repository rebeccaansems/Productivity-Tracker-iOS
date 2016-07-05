using Foundation;
using System;
using System.IO;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using UIKit;
using SQLite;

namespace Productivity_Tracker_iOS
{
    partial class MainViewController : UIViewController
    {

        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(Boolean animated)
        {
            base.ViewDidLoad();

            b_Awesome.TouchUpInside += AwesomeClicked;
            b_Good.TouchUpInside += GoodClicked;
            b_Mediocre.TouchUpInside += MediocreClicked;
            b_Poor.TouchUpInside += PoorClicked;
            b_Terrible.TouchUpInside += TerribleClicked;

            var database = AppDelegate.db.Table<ProductiveData>();
            foreach (var dataPoint in database)
            {
                if (dataPoint.DateHour == DateTime.Now.Hour && dataPoint.DateDay == DateTime.Now.Day && dataPoint.DateMonth == DateTime.Now.Month)
                {
                    DisbleButtons();
                }
            }
        }

        void AwesomeClicked(object sender, EventArgs e)
        {
            ProductiveData p_Data = new ProductiveData { DateHour = DateTime.Now.Hour, DateDay = DateTime.Now.Day, DateMonth = DateTime.Now.Month, ProdutivityLevel = 5 };
            AppDelegate.db.Insert(p_Data);
            DisbleButtons();
        }

        void GoodClicked(object sender, EventArgs e)
        {
            ProductiveData p_Data = new ProductiveData { DateHour = DateTime.Now.Hour, DateDay = DateTime.Now.Day, DateMonth = DateTime.Now.Month, ProdutivityLevel = 4 };
            AppDelegate.db.Insert(p_Data);
            DisbleButtons();
        }

        void MediocreClicked(object sender, EventArgs e)
        {
            ProductiveData p_Data = new ProductiveData { DateHour = DateTime.Now.Hour, DateDay = DateTime.Now.Day, DateMonth = DateTime.Now.Month, ProdutivityLevel = 3 };
            AppDelegate.db.Insert(p_Data);
            DisbleButtons();
        }

        void PoorClicked(object sender, EventArgs e)
        {
            ProductiveData p_Data = new ProductiveData { DateHour = DateTime.Now.Hour, DateDay = DateTime.Now.Day, DateMonth = DateTime.Now.Month, ProdutivityLevel = 2 };
            AppDelegate.db.Insert(p_Data);
            DisbleButtons();
        }

        void TerribleClicked(object sender, EventArgs e)
        {
            ProductiveData p_Data = new ProductiveData { DateHour = DateTime.Now.Hour, DateDay = DateTime.Now.Day, DateMonth = DateTime.Now.Month, ProdutivityLevel = 1 };
            AppDelegate.db.Insert(p_Data);
            DisbleButtons();
        }

        void DisbleButtons()
        {
            b_Awesome.Enabled = false;
            b_Good.Enabled = false;
            b_Mediocre.Enabled = false;
            b_Poor.Enabled = false;
            b_Terrible.Enabled = false;

            b_Awesome.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
            b_Good.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
            b_Mediocre.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
            b_Poor.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
            b_Terrible.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);

            b_Awesome.BackgroundColor = UIColor.FromRGB(43, 43, 43);
            b_Good.BackgroundColor = UIColor.FromRGB(43, 43, 43);
            b_Mediocre.BackgroundColor = UIColor.FromRGB(43, 43, 43);
            b_Poor.BackgroundColor = UIColor.FromRGB(43, 43, 43);
            b_Terrible.BackgroundColor = UIColor.FromRGB(43, 43, 43);
        }
    }
}
