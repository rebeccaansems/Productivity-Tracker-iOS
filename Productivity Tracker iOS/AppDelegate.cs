using Foundation;
using UIKit;
using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public static int minuteMin, minuteMax;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //do you want notifications?
            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert, null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            UIApplication.SharedApplication.CancelAllLocalNotifications();

            //load database
            string dbName = "db.sqlite";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var dbPath = Path.Combine(libraryPath, dbName);
            db = new SQLiteConnection(dbPath);
            db.CreateTable<ProductiveData>();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {

            //remove old notifications
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            UIApplication.SharedApplication.CancelAllLocalNotifications();

            //create/schedule notification
            UILocalNotification notification = new UILocalNotification();
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

            //notification.AlertTitle = "Productivity Tracker"; // required for Apple Watch notifications
            notification.AlertAction = "Log productivity";
            notification.AlertBody = "How productive are you feeling?";
            notification.ApplicationIconBadgeNumber = 1;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        public override void WillEnterForeground(UIApplication application)
        {

        }

        public override void OnActivated(UIApplication application)
        {
        }
    }
}