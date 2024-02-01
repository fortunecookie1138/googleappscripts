/**
 * For more information on using the Spreadsheet API, see
 * https://developers.google.com/apps-script/service_spreadsheet
 */
function readRows() {
  var sheet = SpreadsheetApp.getActiveSheet();
  var rows = sheet.getDataRange();
  var numRows = rows.getNumRows();
  var values = rows.getValues();

  let replacementText = "";
  // uhh might blow up on the last cell with data :shrug: oh well, close enough
  for (var i = 0; i <= numRows; i++) {
    var row = values[i];
    if (row[0] == "") {
      if (values[i-1][0] != "") {
        replacementText = values[i-1][0];
      }
      const cellName = "A"+(i+1).toString();
      Logger.log("cell name: " + cellName);
      const cell = sheet.getRange(cellName);
      Logger.log("Replacing " + cellName + " with: " + replacementText);

      cell.setValue(replacementText);
    }
  }
};

/**
 * Adds a custom menu to the active spreadsheet, containing a single menu item
 * for invoking the readRows() function specified above.
 * The onOpen() function, when defined, is automatically invoked whenever the
 * spreadsheet is opened.
 * For more information on using the Spreadsheet API, see
 * https://developers.google.com/apps-script/service_spreadsheet
 */
function onOpen() {
  var sheet = SpreadsheetApp.getActiveSpreadsheet();
  var entries = [{
    name : "For a single column sheet, fill in each blank cell with the value above it",
    functionName : "readRows"
  }];
  sheet.addMenu("Script Center Menu", entries);
};