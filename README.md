# Assignment8TestSeleniumTests
This is a project that contains the Selenium tests to be run against the mario pizza webinterface

# What is it
This is a C# dotnet-core project that runs the Selenium webdriver against a running webinterface of MariosPizza, on losthost:8080

# Setup
First make sure you have MariosPizza webinterface up and running - do this by following the instructions here [Setup MariosPizza](https://github.com/cph-js284/Assignment8Test)

If MariosPizza is running correctly you can open a browser and navigate to http://localhost:8080 <br>
<br>
<b>Now to run the Selenium tests</b><br>
1) Clone the repo *(Note: do not clone this repo into the same repo containing the webinterface)*
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

# what is tested
The webinterface is tested for the following:
```
Test                                            Expected                            Actual

FROM HOME(ROOT)
Connection to localhost:8080                    Correct page title                  As expected
Connection to localhost:8080/home               Correct page title                  As expected
Connection to localhost:8080/Order              Correct page title                  As expected
Connection to localhost:8080/privacy            Correct page title                  As expected
Click on the link to page:home                  Follows links to correct page       As expected
Click on the link to page:Order                 Follows links to correct page       As expected
Click on the link to page:privacy               Follows links to correct page       As expected


FROM ORDER
Button "Show all orders"                        Shows page containing all orders    As expected
Button "Place new order"                        Shows page w/new orderform          As expected

FROM PLACEORDER
Fill in only name                               Customer name field is filled       As expected
Choose only pizza from dropdown                 Selected pizza contained in dropd   As expected
Choose only pizza from dropdown, click add      Selection displayed in orderarea    As expected
Fill in name, click submit order                Approriate errormsg displayed       As expected
Select pizza,add, click submit order            Approriate errormsg displayed       As expected
Fill name, select pizza, add, submit            Order is approved                   As expected

FROM SHOWALLORDER
Remove order, clicked                           Order is removed from system        As expected
```

