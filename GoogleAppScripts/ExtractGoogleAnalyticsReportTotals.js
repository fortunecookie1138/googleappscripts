var destinationSpreadsheetId = "1Up0YA4bVXsXcRdMAR_berO6KJd1iqXaTLoLuZMlNEpc";
var throwErrors = false;

function onOpen() {
  SpreadsheetApp.getUi()
       .createMenu("Export Report Totals")
       .addItem("Run Export", "ExtractReportValues")
       .addItem("Create export schedule", "CreateExportTrigger")
       .addItem("Delete export schedule", "DeleteExportTrigger")  
       .addToUi();
}

function CreateExportTrigger() {
  var ui = SpreadsheetApp.getUi();
  var hourToRun = ui.prompt("What hour should export run on Mondays?").getResponseText();
  var minuteToRun = ui.prompt("What minute (0, 15, 30, 45) should export run?").getResponseText();
  ScriptApp.newTrigger('ExtractReportValues')
      .timeBased()
      .onWeekDay(ScriptApp.WeekDay.MONDAY)
      .atHour(hourToRun)
      .nearMinute(minuteToRun)
      .create();
}

function DeleteExportTrigger() {
  var triggers = ScriptApp.getProjectTriggers();
  var ui = SpreadsheetApp.getUi();
  var response = ui.prompt("Are you sure you want to delete the " + triggers.length + " triggers for this spreadsheet?",  ui.ButtonSet.YES_NO).getSelectedButton();
  
  if (response == ui.Button.YES) {
    for (var i = 0; i < triggers.length; i++) {
      ScriptApp.deleteTrigger(triggers[i]);
    }
  }  
}

function ExtractReportValues() {
  var sourceSpreadsheet = SpreadsheetApp.getActiveSpreadsheet();
  var destinationSpreadsheet = SpreadsheetApp.openById(destinationSpreadsheetId);
  Logger.log("Source spreadsheet: " + sourceSpreadsheet.getName());
  Logger.log("Destination spreadsheet: " + destinationSpreadsheet.getName());
  Logger.log("Total source sheets: " + sourceSpreadsheet.getSheets().length);
  
  var weekStartDate = GetWeekStartDate(sourceSpreadsheet);
  Logger.log("Week start date: " + weekStartDate);
  
  var rowIndexToWrite = destinationSpreadsheet.getSheets()[0].getLastRow()+1;
  Logger.log("Row Index to write: " + rowIndexToWrite);
  
  WriteWeekStartDate(destinationSpreadsheet, rowIndexToWrite, weekStartDate);
  
  var reportValues = GetReportValues(sourceSpreadsheet);
  WriteReportValues(destinationSpreadsheet, rowIndexToWrite, reportValues);
}

function GetWeekStartDate(sourceSpreadsheet) {
  var allSourceSheets = sourceSpreadsheet.getSheets();
  return allSourceSheets[0].getRange("B4").getValue();
}

function WriteWeekStartDate(destinationSpreadsheet, rowIndexToWrite, dateToWrite) {
  var destinationRange = destinationSpreadsheet.getSheets()[0].getRange("A"+rowIndexToWrite);
  destinationRange.setValue(dateToWrite);
}

function GetReportValues(sourceSpreadsheet) {
  var sheets = sourceSpreadsheet.getSheets();
  var reportValues = {};
  // start at 1 to skip the config sheet
  for (var i = 1; i < sheets.length; i++)
  {
    var sheetName = sheets[i].getName();
    var sheetReportValue = sheets[i].getRange("A12").getValue();
    reportValues[sheetName] = sheetReportValue;
  }
  return reportValues;
}

function WriteReportValues(destinationSpreadsheet, rowIndexToWrite, reportValues) {
  var summarySheet = destinationSpreadsheet.getSheets()[0];
  var headerColumnIndexes = {};
  // start 2 to skip the date column
  for (var i = 2; i <= summarySheet.getLastColumn(); i++)
  {
    var headerName = summarySheet.getRange(1, i).getValue();
    headerColumnIndexes[headerName] = i;
    Logger.log("Header: " + headerName + ", header column index: " + i);
  }
  
  for (key in reportValues)
  {
    var reportValue = reportValues[key];
    Logger.log("Report Name: " + key + ", report value: " + reportValue);
    var columnIndex = headerColumnIndexes[key];
    if (columnIndex)
    {
      Logger.log("Writing value " + reportValue + " to row: " + rowIndexToWrite + " column: + " + columnIndex);
      summarySheet.getRange(rowIndexToWrite, columnIndex).setValue(reportValue);
    }
    else
    {
      var errorMessage = "Could not find index of destination header for report " + key + " in spreadsheet " + 
        destinationSpreadsheet.getName() + ", sheet " + summarySheet.getName();
      Logger.log(errorMessage);
      if (throwErrors)
      {
        throw errorMessage;
      }
    }
  }
}
