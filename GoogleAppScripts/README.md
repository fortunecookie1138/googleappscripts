
Check out https://developers.google.com/apps-script/service_spreadsheet for coding documentation.

To add a script: open a Google Sheets doc -> Extensions -> Apps Scripts -> Copy your code into Code.gs or whatever.

Assuming your code has an `onOpen` function, select it in the UI dropdown and click the `Run` button to add your macro as a new menu item (after granting authorization). You can also use that dropdown to run the individual functions in your copied code against the sheet you had open when you navigated to `Apps Scripts`. Don't forget, `Logger.log()` is your friend!
