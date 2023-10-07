#!/usr/bin/python

# ChromeDriver woes: make sure it's up to date https://stackoverflow.com/questions/21166408/how-do-i-confirm-im-using-the-right-chromedriver
# or just rename the .exe at C:\Windows and it maybe just works?

from selenium import webdriver
from selenium.webdriver.common.by import By
import time
from openpyxl import Workbook
import re

# IsDebugMode = True
IsDebugMode = False

LoadAllPrograms = True
# LoadAllPrograms = False

class programDetails:
    routePath = ""
    name = ""
    description = ""
    artists = []
    tags = "" # called program details in the website, Lyndsey wants them as a comma-separated list

    def prettyPrint(self):
        print(f"Name: {self.name}\nRoute Path: {self.routePath}\nDescription: {self.description}\nArtists: {self.artists}\nTags: {self.tags}")

def Log(asDebug, message):
    if not asDebug:
        print(message)
    if asDebug and IsDebugMode:
        print(message)

def collectLinksToPrograms(browser):
    containerElement = browser.find_element(By.CLASS_NAME, "bbq-content")
    elements = containerElement.find_elements(By.TAG_NAME, "a")
    Log(True, f"We have {len(elements)} 'a' elements to work with")
    links = []
    for e in elements:
        href = e.get_attribute('href')
        if href is not None:
            links.append(href)
    Log(True, f"We have {len(links)} links to work with")
    return links

def loadMore(browser, previousLinkCount):
    loadMoreDiv = browser.find_element(By.CLASS_NAME, "fl-builder-pagination-load-more")
    loadMoreButton = loadMoreDiv.find_element(By.TAG_NAME, "a")
    if loadMoreButton is not None:
        Log(True, "I'm gonna click the Load More button")
        loadMoreButton.click()
        time.sleep(2)
        linkCount = len(collectLinksToPrograms(browser))
        if linkCount != previousLinkCount:
            Log(True, f"We had {previousLinkCount}, now we have {linkCount}")
            loadMore(browser, linkCount)

def getDataForProgram(browser, linkToProgram):
    browser.get(linkToProgram)
    primarySectionElement = browser.find_element(By.ID, "primary")
    programName = primarySectionElement.find_element(By.TAG_NAME, "h1").text
    Log(True, programName)
    descriptionElement = primarySectionElement.find_element(By.CLASS_NAME, "entry-content")
    programDescription = descriptionElement.text
    Log(True, programDescription)

    secondarySectionElement = browser.find_element(By.ID, "secondary")
    programArtistsContainerElement = secondarySectionElement.find_element(By.CLASS_NAME, "sidebar-program-artists")
    programArtistElements = programArtistsContainerElement.find_elements(By.TAG_NAME, "a")
    artists = []
    for e in programArtistElements:
        artists.append(e.text)
    artists = list( dict.fromkeys(artists) ) # de-dupe artists
    Log(True, artists)

    programDetailsContainerElement = secondarySectionElement.find_element(By.CLASS_NAME, "sidebar-program-details")
    items = programDetailsContainerElement.find_elements(By.TAG_NAME, "li")
    Log(True, f"I see {len(items)} items in program details")
    tags = []
    for e in items:
        tagText = e.text
        Log(True, f"Raw tag text: {tagText}")
        badHeaderElements = e.find_elements(By.XPATH, "h4 | h6")
        if len(badHeaderElements) > 0:
            badHeaderElementText = badHeaderElements[0].text
            Log(True, f"badHeaderText: {badHeaderElementText}")
            tagText = tagText.replace(badHeaderElementText, "")
        tags.append(tagText.strip())
    while("" in tags):
        tags.remove("")
    Log(True, tags)

    program = programDetails()
    program.routePath = linkToProgram.replace(rootUrl, "").rstrip("/")
    program.name = programName
    program.description = programDescription
    program.artists = artists
    program.tags = ", ".join(tags)
    program.prettyPrint()
    print("\n")

    return program

def writeHeadersToWorkbook(sheet):
    sheet["A1"] = "Product ID [Non Editable]"
    sheet["B1"] = "Variant ID [Non Editable]"
    sheet["C1"] = "Product Type [Non Editable]"
    sheet["D1"] = "Product Page"
    sheet["E1"] = "Product URL"
    sheet["F1"] = "Title"
    sheet["G1"] = "Description"
    sheet["H1"] = "SKU"
    sheet["I1"] = "Option Name 1"
    sheet["J1"] = "Option Value 1"
    sheet["K1"] = "Option Name 2"
    sheet["L1"] = "Option Value 2"
    sheet["M1"] = "Option Name 3"
    sheet["N1"] = "Option Value 3"
    sheet["O1"] = "Option Name 4"
    sheet["P1"] = "Option Value 4"
    sheet["Q1"] = "Option Name 5"
    sheet["R1"] = "Option Value 5"
    sheet["S1"] = "Option Name 6"
    sheet["T1"] = "Option Value 6"
    sheet["U1"] = "Price"
    sheet["V1"] = "Sale Pricee"
    sheet["W1"] = "On Sale"
    sheet["X1"] = "Stock"
    sheet["Y1"] = "Categories"
    sheet["Z1"] = "Tags"
    sheet["AA1"] = "Weight"
    sheet["AB1"] = "Length"
    sheet["AC1"] = "Width"
    sheet["AD1"] = "Height"
    sheet["AE1"] = "Visible"
    sheet["AF1"] = "Hosted Image URLs"
    
def writeProgramToWorkbook(sheet, index, program):
    sheet["A"+str(index)] = ""
    sheet["B"+str(index)] = ""
    sheet["C"+str(index)] = "SERVICE"
    sheet["D"+str(index)] = "arts-programs"
    sheet["E"+str(index)] = program.routePath
    sheet["F"+str(index)] = program.name
    sheet["G"+str(index)] = program.description
    sheet["H"+str(index)] = "SQ4218371"
    sheet["I"+str(index)] = ""
    sheet["J"+str(index)] = ""
    sheet["K"+str(index)] = ""
    sheet["L"+str(index)] = ""
    sheet["M"+str(index)] = ""
    sheet["N"+str(index)] = ""
    sheet["O"+str(index)] = ""
    sheet["P"+str(index)] = ""
    sheet["Q"+str(index)] = ""
    sheet["R"+str(index)] = ""
    sheet["S"+str(index)] = ""
    sheet["T"+str(index)] = ""
    sheet["U"+str(index)] = "0"
    sheet["V"+str(index)] = "0"
    sheet["W"+str(index)] = "No"
    sheet["X"+str(index)] = "Unlimited"
    sheet["Y"+str(index)] = program.tags
    sheet["Z"+str(index)] = ""
    sheet["AA"+str(index)] = "0"
    sheet["AB"+str(index)] = "0"
    sheet["AC"+str(index)] = "0"
    sheet["AD"+str(index)] = "0"
    sheet["AE"+str(index)] = "Yes"
    sheet["AF"+str(index)] = ""
    index +=1

    return index

rootUrl = "https://arts-inspiredlearning.org/programs/"
browser = webdriver.Chrome()
browser.get(rootUrl)

if LoadAllPrograms:
    loadMore(browser, 0)

allProgramLinks = collectLinksToPrograms(browser)
Log(False, allProgramLinks)

workbook = Workbook()
sheet = workbook.active
writeHeadersToWorkbook(sheet)

index = 2
for link in allProgramLinks:
    program = getDataForProgram(browser, link)
    index = writeProgramToWorkbook(sheet, index, program)

workbook.save(filename="ArtsInspiredLearningScraped.xlsx")

browser.quit()