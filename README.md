# Assignment8TestSeleniumTests
This is a project that contains the Selenium tests to be run against the mario pizza webinterface

# What is it
This is a C# dotnet-core project that runs the Selenium webdriver against a running webinterface of MariosPizza, on losthost:8080

# Setup
First make sure you have MariosPizza webinterface up and running - do this by following the instructions here [Setup MariosPizza](https://github.com/cph-js284/Assignment8Test)

If MariosPizza is running correctly you can open a browser and navigate to http://localhost:8080 <br>
<br>
<b>Now to run the Selenium tests</b><br>
1) Clone the repo
2) if you dont have dotnet-core installed, do so by executing
```
sudo snap install dotnet-sdk --classic
```
3) Build the project by executing
```
sudo dotnet-sdk.dotnet build
```
4) Run the Selenium tests by executing 
```
sudo dotnet-sdk.dotnet test
```

*This will create a folder inside the project called "ScreenShots". Each screenshot share the name of the specific test that created it. The Screenshots are meant to serve as an extra layer of documentation*

