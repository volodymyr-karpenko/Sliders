namespace Sliders.Core.Constants
{
    public struct AppConstants
    {
        public const string appDescription = "This is a sample cross-platform app for Android Tablet/iOS iPad that is built with ASP.NET Core API, " + 
            "Xamarin and MvvmCross. " + "\r\n\r\n" +
            "IDEA: Let's assume that you have an IoT device that measures some environment once per second. To give you a better understanding " +
            "of the environment's state at a certain point in time, each measurement consists of five markers presented as sliders. When each measurement " +
            "is completed, an IoT device sends the data to a database in the following format: measurement ID, measurement timestamp in the UTC " +
            "format, slider 1, slider 2, slider 3, slider 4, slider 5. " + "\r\n\r\n" +
            "OBJECTIVE: Create a server-side logic - the ASP.NET Core API that talks to a database and a client-side logic - the Xamarin MVVM mobile " +
            "application that talks to the API. A client-side logic requests the latest available measurement from a given database, then it presents that " +
            "measurement to the user for one second, and after that it requests another measurement, and so forth. Other than that a client-side logic " +
            "also creates random measurement data every second and sends it to a database because there is no actual IoT device." + "\r\n\r\n" +
            "UI: When the \"START SESSION\" label is tapped, the app starts to generate random measurement data from -200 to 200 for five sliders " +
            "and write this data to a database and read it afterward. When the \"STOP SESSION\" is tapped, the app stops generating, writing and reading " +
            "the data. When the \"Trash\" icon is tapped, the app deletes all of the entries in a database to prevent storing randomly generated data on your computer. " + 
            "In case you want to see the data in a database you need to go to your phpMyAdmin, open the \"slidersdb\" database, open the \"slidersdata\" table and refresh your browser.";
    }
}