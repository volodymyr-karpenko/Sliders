# Sliders
Sample cross-platform app for Android Tablet/iOS iPad that is built with ASP.NET Core API, Xamarin and MvvmCross

### IDEA: 
Let's assume that you have an IoT device that measures some environment once per second. To give you a better 
understanding of the environment's state at a certain point in time, each measurement consists of five markers presented as sliders. 
When each measurement is completed, an IoT device sends the data to a database in the following format: measurement ID, measurement 
timestamp in the UTC format, slider 1, slider 2, slider 3, slider 4, slider 5.

### OBJECTIVE: 
Create a server-side logic - the ASP.NET Core API that talks to a database and a client-side logic - the Xamarin MVVM mobile 
application that talks to the API. A client-side logic requests the latest available measurement from a given database, then it presents 
that measurement to the user for one second, and after that it requests another measurement, and so forth. Other than that a client-side 
logic also creates random measurement data every second and sends it to a database because there is no actual IoT device.

### UI: 
When the "START SESSION" label is tapped, the app starts to generate random measurement data from -200 to 200 for five sliders and 
write this data to a database and read it afterward. When the "STOP SESSION" is tapped, the app stops generating, writing and reading the 
data. When the "Trash" icon is tapped, the app deletes all of the entries in a database to prevent storing randomly generated data on 
your computer. In case you want to see the data in a database you need to go to your phpMyAdmin, open the "slidersdb" database, open the 
"slidersdata" table and refresh your browser.

## Prerequisites 
In order to run the application, you need the following software to be up and running on your computer (both PC and MAC are eligible)
1. Visual Studio
2. XAMPP

### Visual Studio solution set up
1. After you successfully cloned this project on your machine, open the solution and set "Sliders.API" as a startup project. DO NOT run this 
project, instead, you have to change the start option from "IIS Express" to "Sliders.API".
2. Set "Sliders.Forms.Droid" (PC) or "Sliders.Forms.iOS" (MAC) as a startup project. DO NOT run this project, instead, you have to change 
the start option to your Tablet/iPad device emulator/simulator.
3. Right-click on the solution at the very top of the Solution Explorer and click "Set StartUp Projects...", choose "Multiple startup projects" 
and change "None" to "Start" for "Sliders.API" and "Sliders.Forms.Droid" (PC) or "Sliders.Forms.iOS" (MAC). You should end up with 
only two projects of the solution set to "Start" the rest of them have to be set to "None". Click "Apply" and then "OK" button.
4. Open "Sliders.API" -> "Startup.cs" and check if the MariaDB version stated there is the same as the one installed on your computer during 
the XAMPP installation if it is not, then you should indicate the MariaDB version that is installed on your machine.

### XAMPP set up
1. Run XAMPP and ensure that both Apache and MySQL are up and running.
2. Open your browser and go to http://localhost/phpmyadmin click "User accounts" and then "Add user account" and create a new user 
with credentials from the "appsettings.json" file of the "Sliders.API" project (User id and Password). Set the "Hostname" option 
to the "localhost", then go down to "Global privileges" and click "Check all" checkbox. Click the "Go" button at the bottom right corner. 

Go back to your Visual Studio and click the "Start" button. Click it one more time if you get an error during the first try.
When your browser has opened right after "localhost:5001" in the address line type /api/SlidersData/test 
(you should end up with this "https://localhost:5001/api/SlidersData/test"), hit Enter and you should see the fist entry written to the database 
during startup for testing purposes, it should look something like this:
{"id":"test","time":"2020-03-06T13:30:39.884042","slider1":-100,"slider2":-50,"slider3":0,"slider4":50,"slider5":100}

On your mobile device emulator, at the top of the screen, you should see "Total measurements: 1".

[Android emulator screenshot](screenshots/img_screenshot.png)