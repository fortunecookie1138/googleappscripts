var UseDefaultColumns = true;
var MaxRowsToGet = 1000;

function onOpen() {
  SpreadsheetApp.getUi()
      .createMenu("Lyndsey's Menu")
      .addItem("Compare Two Columns", "CompareColumns")
      .addToUi();
}

function CompareColumns() {
  var currentSheet = SpreadsheetApp.getActiveSpreadsheet().getSheets()[0];
  var columnLetters = GetColumnLettersToCompare();
  var firstColumnArray = GetListData(currentSheet, columnLetters[0], MaxRowsToGet);
  var secondColumnArray = GetListData(currentSheet, columnLetters[1], MaxRowsToGet);
  
  var matchingValuesArray = GetMatchingValues(firstColumnArray, secondColumnArray);
  
  var destinationColumnLetter = "C";
  ClearDestinationColumn(currentSheet, destinationColumnLetter, MaxRowsToGet);
  WriteOutMatches(currentSheet, destinationColumnLetter, matchingValuesArray);
}

function onlyUnique(value, index, self) { 
    return self.indexOf(value) === index;
}

function GetListData(currentSheet, columnLetter, rowsToRetrieve) {
  var sheetName = currentSheet.getName();
  // TODO replace hard-coded column height
  var rangeName = sheetName+"!"+columnLetter+"1:"+columnLetter+rowsToRetrieve;
  Logger.log("Range name: " + rangeName);
  var dataRange = currentSheet.getRange(rangeName);
    
  var rangeValues = dataRange.getValues(); // returns 2-D array of [col][row]
  var cellData = [];
  var logData = "";
  for (var i = 0; i < dataRange.getHeight(); i++)
  {
    // TODO trim data for everything here, or only for comparison purposes down below?
    var cellValue = rangeValues[i][0].trim();
    if (cellValue)
    {
      cellData.push(cellValue);
    }
  }
  Logger.log("Number of cells with data: " + cellData.length);
  // TODO see if i can do this without pushing all values into cellData array first
  var uniqueValues = cellData.filter( onlyUnique );
  Logger.log("Number of unique cell values: " + uniqueValues.length);
  
  return uniqueValues;
}

function GetMatchingValues(firstArray, secondArray) {
  var matchingValues = [];
  for (var i = 0; i < firstArray.length; i++){
    // TODO figure out better way to do case insensitive check
    var uppperCaseSecondArray = ArrayValuesToUpper(secondArray);
    if (uppperCaseSecondArray.indexOf(firstArray[i].toUpperCase()) > -1){
      Logger.log("Match found: " + firstArray[i]);
        matchingValues.push(firstArray[i]);
      }
    }
  Logger.log("Total matches found: " + matchingValues.length);
  
  return matchingValues;
}

function ArrayValuesToUpper(arrayToConvert) {
  var outputArray = [];
  for (var i = 0; i < arrayToConvert.length; i++)
  {
    outputArray.push(arrayToConvert[i].toUpperCase());
  }
  return outputArray;
}

function ClearDestinationColumn(currentSheet, columnLetter, rangeLength) {
  var rangeNotation = columnLetter + "1:" + columnLetter + rangeLength;
  Logger.log("Clearing range " + rangeNotation);
  var rangeToClear = currentSheet.getRange(rangeNotation);
  rangeToClear.clear();
}

function WriteOutMatches(currentSheet, columnLetterToWrite, matchingValuesArray) {
  for (var i = 0; i < matchingValuesArray.length; i++)
  {
    var cellName = columnLetterToWrite+(i+1);
    var rangeToWrite = currentSheet.getRange(cellName);
    Logger.log("Writing to " + cellName + ": " + matchingValuesArray[i]);
    rangeToWrite.setValue(matchingValuesArray[i]);
  }
}

function GetColumnLettersToCompare() {
  if (UseDefaultColumns)
  {
    return ["A","B"];
  }
  else
  {
    var ui = SpreadsheetApp.getUi();
    var firstListColumn = ui.prompt("Column letter of first list?").getResponseText();
    if (firstListColumn == "")
    {
      ui.alert("Value not provided, defaulting to A");
      var firstListColumn = "A";
    }
    var secondListColumn = ui.prompt("Column letter of second list?").getResponseText();
    if (secondListColumn == "")
    {
      ui.alert("Value not provided, defaulting to B");
      var firstListColumn = "B";
    }
    return [firstListColumn, secondListColumn];
  }
}
