#!/usr/bin/python

from selenium import webdriver
from openpyxl import Workbook
import re

class parsed:
    location = ""
    routeType = ""
    routeCount = ""
    revenue = ""
    income = ""
    price = ""

numericRegex = "[^0-9]"

def parseAndWrite(routeType, data, index, sheet, printParsed = False):
    for element in data:
        data = parsed()
        data.location = element.find_element_by_class_name("summary-title-link").text
        data.routeType = routeType
        details = element.find_element_by_class_name("summary-excerpt").find_elements_by_tag_name('p')
        data.routeCount = details[0].text
        data.revenue = details[1].text
        data.income = details[2].text
        data.price = details[3].text

        if printParsed:
            print(data.location)
            print(data.routeType)
            print(data.routeCount)
            print(data.revenue)
            print(data.income)
            print(data.price)
        
        sheet["A"+str(index)] = data.location
        sheet["B"+str(index)] = data.routeType
        sheet["C"+str(index)] = re.sub(numericRegex, '', data.routeCount)
        sheet["D"+str(index)] = re.sub(numericRegex, '', data.revenue)
        sheet["E"+str(index)] = re.sub(numericRegex, '', data.income)
        sheet["F"+str(index)] = re.sub(numericRegex, '', data.price)
        index +=1

    return index

workbook = Workbook()
sheet = workbook.active

sheet["A1"] = "Location"
sheet["B1"] = "Route Type"
sheet["C1"] = "Route Count"
sheet["D1"] = "Total Revenue"
sheet["E1"] = "Net Operating Income"
sheet["F1"] = "Listing Price"

browser = webdriver.Chrome()
browser.get("https://www.routeconsultant.com/explore/pd")

pdElements = browser.find_elements_by_class_name('summary-item')

nextIndex = parseAndWrite("P&D", pdElements, 2, sheet, True)

browser.get("https://www.routeconsultant.com/explore/linehaul")

lhElements = browser.find_elements_by_class_name('summary-item')

parseAndWrite("Linehaul", lhElements, nextIndex, sheet, True)

workbook.save(filename="routeConsultantScraped.xlsx")

browser.quit()