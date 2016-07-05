using Foundation;
using UIKit;
using SQLite;
using System;
using System.IO;
using System.Collections.Generic;

namespace Productivity_Tracker_iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        UIWindow window;
        UIViewController root;
        MainViewController main;

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
            //create/schedule notification
            UILocalNotification notification = new UILocalNotification();
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(3600);
            //notification.AlertTitle = "Productivity Tracker"; // required for Apple Watch notifications
            notification.AlertAction = "Log productivity";
            notification.AlertBody = "How productive are you feeling?";
            notification.ApplicationIconBadgeNumber = 1;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}