var hourFormat = "0.00";

function onOpen() {
  SpreadsheetApp.getUi()
      .createMenu("Joe's Menu")
      .addItem("Create Game Tracker Sheet", "GenerateTable")
      .addItem("Reverse", "ReverseOrder")
      .addToUi();
}

function ReverseOrder()
{
  var range = SpreadsheetApp.getActiveRange();
  var values = range.getValues();
  range.setValues(values.reverse());
}

function GenerateTable() {
  var yearToUse = getYear();
  
  if (yearToUse)
  {
    var newSheet = SpreadsheetApp.getActiveSpreadsheet().insertSheet(yearToUse,0);
    CreateHeaders(newSheet);
    SetDates(newSheet, yearToUse);
    FormatHours(newSheet);
    FormatTotals(newSheet);
  }
}

function CreateHeaders(currentSheet) {
  currentSheet.setFrozenRows(1);
  var headers = currentSheet.getRange("A1:C1");
  var headerValues = [["Date", "Hours", "Total"]];
  headers.setValues(headerValues);
  headers.setFontWeight("bold");
  headers.setFontStyle("italic");
  headers.setHorizontalAlignment("center");
}

function SetDates(currentSheet, year) {
  var msSince = Date.parse("1/1/" + year);
  var dateGuy = new Date(msSince);
  var allDates = new Array(365);
  for (i = 0; i < 365; i++) {
    var nextDate = dateGuy.addDays(i);
    allDates[i] = [ formatDate(nextDate) ];
    
    if (nextDate.getDay() == 6)
    {
      setupEndOfWeekRow(currentSheet, i);
    }
  }
  var datesColumn = currentSheet.getRange("A2:A366");
  datesColumn.setValues(allDates);
  datesColumn.setHorizontalAlignment("center");
}

function FormatHours(currentSheet) {
  var hoursColumn = currentSheet.getRange("B2:B366");
  hoursColumn.setNumberFormat(hourFormat);
  hoursColumn.setHorizontalAlignment("center");
}

function FormatTotals(currentSheet) {
  var totalsColumn = currentSheet.getRange("C2:C366"); 
  totalsColumn.setFontWeight("bold");
  totalsColumn.setNumberFormat(hourFormat);
  totalsColumn.setHorizontalAlignment("center");
}

function getYear() {
  var ui = SpreadsheetApp.getUi();
  var response = ui.prompt("Year to use for tracking table:");
  var yearToUse = response.getResponseText();
  
  if (isNaN(yearToUse))
  {
    ui.alert("\"" + yearToUse + "\" is not a valid year value!");
    return null;
  }
  return yearToUse;
}

function formatDate(fullDate) {
  var dayNames = ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
  return (fullDate.getMonth()+1) + "/" + fullDate.getDate() + "/" 
     + fullDate.getFullYear() + ": " + dayNames[fullDate.getDay()];
}

function setupEndOfWeekRow(currentSheet, dateIndex) {
  var weekEndIndex = dateIndex + 2;
  var endOfWeekRow = currentSheet.getRange("A"+weekEndIndex+":C"+weekEndIndex);
  endOfWeekRow.setBorder(false, false, true, false, false, false);
  
  var weekStartIndex = weekEndIndex - 6;  
  if (weekStartIndex < 2)
  {
    weekStartIndex = 2;
  }  
  var endOfWeekTotalCell = currentSheet.getRange("C"+weekEndIndex);
  endOfWeekTotalCell.setFormula("=SUM(B"+weekStartIndex+":B"+weekEndIndex+")");
}  

Date.prototype.addDays = function(days) {
  var d = new Date(this.valueOf());
  d.setDate(d.getDate() + days);
  return d;
}