function onOpen() {
  SpreadsheetApp.getUi()
       .createMenu("Format Form Timestamp")
       .addItem("Create auto format trigger", "CreateTrigger")
       .addItem("Delete auto format trigger", "DeleteTrigger")  
       .addToUi();
}

function CreateTrigger() {
  ScriptApp.newTrigger('FormatTimestampOnSubmission')
      .forSpreadsheet(SpreadsheetApp.getActive())
      .onFormSubmit()
      .create();
  
  SpreadsheetApp.getUi().alert("Auto format date field trigger created");
}

function DeleteTrigger() {
  var triggers = ScriptApp.getProjectTriggers();
  var ui = SpreadsheetApp.getUi();
  var response = ui.prompt("Are you sure you want to delete the " + triggers.length + " trigger(s) for this spreadsheet?",  ui.ButtonSet.YES_NO).getSelectedButton();
  
  if (response == ui.Button.YES) {
    for (var i = 0; i < triggers.length; i++) {
      ScriptApp.deleteTrigger(triggers[i]);
    }
  }  
}

function FormatTimestampOnSubmission(e) {
  e.range.getCell(1,1).setNumberFormat("m/d");
}