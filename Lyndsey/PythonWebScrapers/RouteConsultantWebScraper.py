#!/usr/bin/python

###############################
# 
# Ultimately this implementation didn't work because the site only returns the first 30 locations when you retrieve the data this way.
# I switched to selenium so I could load the entire page and then retrieve ALL data that way.
#
##############################

import time
from bs4 import BeautifulSoup
import requests
from openpyxl import Workbook

class parsed:
    location = ""
    routeCount = ""
    revenue = ""
    income = ""
    price = ""

includeLineHaul = False
printParsed = False

def parseAndWrite(data, index, sheet):
  for record in data:
      data = parsed()
      data.location = record.find('a', attrs={"class":"summary-title-link"}).text
      summary = record.find('div', attrs={"class":"summary-excerpt"})
      
      data.routeCount = summary.contents[0].text
      data.revenue = summary.contents[1].text
      data.income = summary.contents[2].text
      if contentLength > 3:
        data.price = summary.contents[3].text
      else:
        data.price = "Listing Price: $Unknown"

      if printParsed:
        print(summary)
        print(data.location)
        print(data.routeCount)
        print(data.revenue)
        print(data.income)
        print(data.price)

      sheet["A"+str(index)] = data.location
      sheet["B"+str(index)] = data.routeCount[19:]
      sheet["C"+str(index)] = data.revenue[16:]
      sheet["D"+str(index)] = data.income[23:]
      sheet["E"+str(index)] = data.price[16:]
      index +=1
    
  return index

url = "https://www.routeconsultant.com/explore/pd"
response = requests.get(url, timeout=5)
content = BeautifulSoup(response.content, "html.parser")

routeTiles = content.findAll('div', attrs={"class":"summary-item"}, limit=50)

workbook = Workbook()
sheet = workbook.active

sheet["A1"] = "Location"
# sheet["B1"] = "Route Type"
sheet["B1"] = "Route Count"
sheet["C1"] = "Total Revenue"
sheet["D1"] = "Net Operating Income"
sheet["E1"] = "Listing Price"

nextIndex = parseAndWrite(routeTiles, 2, sheet)

if includeLineHaul:
  linehaulUrl = "https://www.routeconsultant.com/explore/linehaul"
  linehaulResponse = requests.get(linehaulUrl, timeout=5)
  linehaulContent = BeautifulSoup(linehaulResponse.content, "html.parser")

  linehaulData = linehaulContent.findAll('div', attrs={"class":"summary-item"})

  parseAndWrite(linehaulData, nextIndex, sheet)


timestamp = time.strftime("%Y%m%d-%H%M%S")

workbook.save(filename="routeConsultantScraped_"+timestamp+".xlsx")