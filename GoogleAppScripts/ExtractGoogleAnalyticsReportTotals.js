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
  Logger.log("Source spreadsheet: %s", sourceSpreadsheet.getName());
  Logger.log("Destination spreadsheet: %s", destinationSpreadsheet.getName());
  
  var weekStartDate = GetWeekStartDate(sourceSpreadsheet);
  Logger.log("Week start date: %s", weekStartDate);
  
  var rowIndexToWrite = destinationSpreadsheet.getSheets()[0].getLastRow()+1;
  Logger.log("Row Index to write: %s", rowIndexToWrite);
  
  WriteWeekStartDate(destinationSpreadsheet, rowIndexToWrite, weekStartDate);
  
  var reportValues = GetReportValues(sourceSpreadsheet);
  var headerColumnIndexes = GetHeaderColumnIndexes(destinationSpreadsheet);
  WriteReportValues(destinationSpreadsheet, headerColumnIndexes, rowIndexToWrite, reportValues);
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
    var sheetReportValue = sheets[i].getRange("A12").getValue(); // the magic cell where the total always is
    reportValues[sheetName] = sheetReportValue;
  }
  return reportValues;
}

function GetHeaderColumnIndexes(destinationSpreadsheet) {
  var summarySheet = destinationSpreadsheet.getSheets()[0];
  var headerColumnIndexes = {};
  // start 5 to skip the date and week total columns
  for (var i = 5; i <= summarySheet.getLastColumn(); i++)
  {
    var headerName = summarySheet.getRange(1, i).getValue();
    headerColumnIndexes[headerName] = i;
    Logger.log("Header: %s, header column index: %s", headerName, i);
  }
  return headerColumnIndexes;
}

function WriteReportValues(destinationSpreadsheet, headerColumnIndexes, rowIndexToWrite, reportValues) {
  var summarySheet = destinationSpreadsheet.getSheets()[0];
  var currentWeekTotal = 0;
  var weekLastYearTotal = 0;
  
  for (key in reportValues)
  {
    var reportValue = reportValues[key];
    Logger.log("Report Name: %s, report value: %s", key, reportValue);
    var columnIndex = headerColumnIndexes[key];
    if (columnIndex)
    {
      Logger.log("Writing value %s to row: %s column: %s", reportValue, rowIndexToWrite, columnIndex);
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
    
    if (key.match(/last year/i))
    {
      weekLastYearTotal += reportValue;
    }
    else
    {
      currentWeekTotal += reportValue;
    }
  }
  
  var percentageChange = DeterminePercentageChange(currentWeekTotal, weekLastYearTotal);
  var totalsRange = summarySheet.getRange(rowIndexToWrite, 2, 1, 3);
  totalsRange.setValues([[ currentWeekTotal, weekLastYearTotal, percentageChange ]]);
}

function DeterminePercentageChange(currentTotal, lastYearTotal) {
  Logger.log("Current week total: %s, week last year total: %s", currentTotal, lastYearTotal);
  
  var sign = "";
  var percentChange = 0;
  if (currentTotal > lastYearTotal)
  {
    sign = "+";
    percentChange = (currentTotal - lastYearTotal) / currentTotal * 100;
  }
  else if (currentTotal < lastYearTotal)
  {
    sign = "-";
    percentChange = (lastYearTotal - currentTotal) / lastYearTotal * 100;
  }
  
  var returnValue = sign + percentChange;
  Logger.log("Percentage change: %s", returnValue);
  
  return returnValue;
}
