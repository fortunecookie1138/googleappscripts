function onOpen() {
  SlidesApp.getUi()
       .createMenu("Chart Refresh")
       .addItem("Refresh all charts", "RefeshCharts")
       .addItem("Create chart refresh schedule", "CreateChartRefreshTrigger")
       .addItem("Delete chart refresh schedule", "DeleteChartRefreshTrigger")  
       .addToUi();
}

function RefeshCharts()
{
    var presentationSlides = SlidesApp.getActivePresentation().getSlides();
    for(var i = 0; i < presentationSlides.length; i++)
    {
      var currentSlide = presentationSlides[i];
      var slideCharts = currentSlide.getSheetsCharts();
      for (var j = 0; j < slideCharts.length; j++)
      {
        var chart = slideCharts[j];
        chart.refresh();
      }
    }
}

function CreateChartRefreshTrigger() {
  var refreshIntervalMinutes = SlidesApp.getUi().prompt("How many minutes between chart refresh?").getResponseText();
  ScriptApp.newTrigger('RefeshCharts')
      .timeBased()
      .everyMinutes(refreshIntervalMinutes)
      .create();
}

function DeleteChartRefreshTrigger() {
  var triggers = ScriptApp.getProjectTriggers();
  var ui = SlidesApp.getUi();
  var response = ui.prompt("Are you sure you want to delete the " + triggers.length + " triggers for this presentation?",  ui.ButtonSet.YES_NO).getSelectedButton();
  
  if (response == ui.Button.YES) {
    for (var i = 0; i < triggers.length; i++) {
      ScriptApp.deleteTrigger(triggers[i]);
    }
  }  
}