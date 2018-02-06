function onOpen() {
  SpreadsheetApp.getUi()
       .createMenu("Format Form Timestamp")
       .addItem("Create auto format trigger", "CreateTrigger")
       .addItem("Delete auto format trigger", "DeleteTrigger")  
       .addToUi();
}

function CreateTrigger() {
  var existingTriggers = ScriptApp.getProjectTriggers();
  var ui = SpreadsheetApp.getUi();
  if (existingTriggers.length == 0)
  {
    ScriptApp.newTrigger('FormatTimestampOnSubmission')
      .forSpreadsheet(SpreadsheetApp.getActive())
      .onFormSubmit()
      .create();
  
  ui.alert("Auto format date field trigger created");
  }
  else
  {
    ui.alert("Did not create auto format date field trigger because it already exists");
  }
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