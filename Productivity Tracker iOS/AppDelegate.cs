using Foundation;
using UIKit;
using SQLite;
using System;
using System.IO;

namespace Productivity_Tracker_iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public static SQLiteConnection db;

        public static int hourMin = 8, hourMax = 12 + 10;
        public static int minuteMin = 0, minuteMax = 0;

        private UILocalNotification notification;

        public override void OnActivated(UIApplication application)
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                if (UIScreen.MainScreen.Bounds.Size.Height == 480)
                {
                    Window = new UIWindow(UIScreen.MainScreen.Bounds);
                    UIStoryboard board = UIStoryboard.FromName("Main4s", null);
                    UIViewController baslangic = (UIViewController)board.InstantiateViewController("TabBarController");

                    Window.RootViewController = baslangic;
                    Window.MakeKeyAndVisible();
                }
                else
                {
                    Window = new UIWindow(UIScreen.MainScreen.Bounds);
                    UIStoryboard board = UIStoryboard.FromName("Main", null);
                    UIViewController baslangic = (UIViewController)board.InstantiateViewController("TabBarController");

                    Window.RootViewController = baslangic;
                    Window.MakeKeyAndVisible();
                }
            }

        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //do you want notifications?
            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.CancelAllLocalNotifications();

            //load database
            string dbName = "db.sqlite";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var dbPath = Path.Combine(libraryPath, dbName);
            db = new SQLiteConnection(dbPath);
            db.CreateTable<ProductiveData>();

            db.CreateTable<MinMaxTimes>();
            if (db.Table<MinMaxTimes>().Count() == 0)
            {
                MinMaxTimes times = new MinMaxTimes();
                times.MinHour = hourMin;
                times.MaxHour = hourMax;
                times.MinMinute = minuteMin;
                times.MaxMinute = minuteMax;
                db.Insert(times);
            }
            else
            {
                MinMaxTimes times = db.Table<MinMaxTimes>().First();
                hourMin = times.MinHour;
                hourMax = times.MaxHour;
                minuteMin = times.MinMinute;
                minuteMax = times.MaxMinute;
            }

            return true;
        }

        public override void DidEnterBackground(UIApplication application)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            notification = new UILocalNotification();

            //notification.AlertTitle = "Productivity Tracker"; // required for Apple Watch notifications
            notification.AlertAction = "Log productivity";
            notification.AlertBody = "How productive are you feeling?";
            notification.ApplicationIconBadgeNumber = 1;

            DateTime hourHalfFromNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0);

            if (DateTime.Now.Hour + 1 <= hourMax)
            {
                if (DateTime.Now.Hour + 1 == hourMax)
                {
                    hourHalfFromNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour + 1, minuteMax, 0);
                }
                else
                {
                    hourHalfFromNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour + 1, 30, 0);
                }
            }
            else if (DateTime.Now.Hour + 1 > hourMax)
            {
                hourHalfFromNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, hourMin, minuteMin, 0);
            }

            notification.FireDate = NSDate.FromTimeIntervalSinceNow(hourHalfFromNow.Subtract(DateTime.Now).TotalSeconds);

            UIApplication.SharedApplication.ScheduleLocalNotification(notification);

            //Save min/max times in database
            MinMaxTimes times = new MinMaxTimes();
            times.MinHour = hourMin;
            times.MaxHour = hourMax;
            times.MinMinute = minuteMin;
            times.MaxMinute = minuteMax;
            db.DeleteAll<MinMaxTimes>();
            db.Insert(times);
        }
    }
}