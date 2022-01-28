#!/usr/bin/python

import sys
import requests
import json
import time
from PyPDF4 import PdfFileReader, PdfFileWriter

apiKey = '18ab7fe66f4a5fcd7a8f06896dbb8c672d781548e44bab1770b182c9924bce59'
getSignatures = 'https://' + apiKey + ':@api.hellosign.com/v3/signature_request/list'
getFiles = 'https://' + apiKey + ':@api.hellosign.com/v3/signature_request/files'
headers = {'authorization': "Basic MThhYjdmZTY2ZjRhNWZjZDdhOGYwNjg5NmRiYjhjNjcyZDc4MTU0OGU0NGJhYjE3NzBiMTgyYzk5MjRiY2U1OTo=", 'accept': "application/json"}
rootOutoutPath = 'C:\src\personalthings\Lyndsey\HelloSignSignatures\CompletedFiles'

print(f'Using root output path {rootOutoutPath}\n')

def getSignatureData(page = 1):
  response = requests.get(getSignatures + '?page=' + str(page), headers=headers)
  loaded = json.loads(response.text)
  return loaded

def getSignatureFile(signatureId, destinationPath):
  response = requests.get(getFiles + '/' + signatureId, headers=headers)
  if response.status_code != 200:
    print(f'Failed to get file for signatureId {signatureId}, API responded with {response.status_code}: {response.text}')
    sys.exit()
  else:
    with open(destinationPath, 'wb') as f: # 'w' = write, 'b' = binary
      f.write(response.content)

# PDF functions stolen from https://realpython.com/pdf-python/
def extractPdfPage(path, pageIndex, extractedPdfName):
  pdf = PdfFileReader(path)
  for page in range(pdf.getNumPages()):
    if page != pageIndex:
      continue
    pdf_writer = PdfFileWriter()
    pdf_writer.addPage(pdf.getPage(page))

    with open(extractedPdfName, 'wb') as output_pdf:
      pdf_writer.write(output_pdf)

def mergePdfFiles(paths, outputPath):
  pdf_writer = PdfFileWriter()

  for path in paths:
    pdf_reader = PdfFileReader(path)
    for page in range(pdf_reader.getNumPages()):
      pdf_writer.addPage(pdf_reader.getPage(page))

  with open(outputPath, 'wb') as out:
    pdf_writer.write(out)

firstPage = getSignatureData()
pageCount = firstPage["list_info"]["num_pages"]

def downloadAndSplitFile(signatureId):
  rawOutputPath = f'{rootOutoutPath}\Raw\{signatureId}_RAW.pdf'
  print(f'Getting completed file for signatureId {signatureId}')
  getSignatureFile(signatureId, rawOutputPath)
  pageToExtract = 1 # zero-based index, Lyndsey wants the second page with the signature on it
  splitPagePath = f'{rootOutoutPath}\Split\{signatureId}_Page{str(pageToExtract+1)}.pdf'
  extractPdfPage(rawOutputPath, pageToExtract, splitPagePath)
  return splitPagePath

# first value of argv is the name of the script being executed
maxPage = int(sys.argv[1]) if len(sys.argv) > 1 else pageCount

singleSignatureId = str(sys.argv[2]) if len(sys.argv) > 2 else None

currentPage = 1
retrievedFileCount = 0
splitFilePaths = []

if singleSignatureId is not None:
  print(f'Running script for single signatureId {singleSignatureId}')
  downloadAndSplitFile(singleSignatureId)
else:
  while currentPage <= maxPage:
    print(f'Current page: {str(currentPage)}/{maxPage}')
    pageData = getSignatureData(currentPage)
    signatures = pageData["signature_requests"]
    completeSignatureIds = [s["signature_request_id"] for s in filter(lambda x: x["is_complete"] == True, signatures)]
    for signatureId in completeSignatureIds:
      splitPath = downloadAndSplitFile(signatureId)
      splitFilePaths.append(splitPath)
      retrievedFileCount += 1
      if retrievedFileCount % 25 == 0:
        print('Hit the API 25 times, sleeping for a minute to avoid HTTP 429...')
        time.sleep(60)
    currentPage += 1

  mergedFilePath = f'{rootOutoutPath}\AllSignedForms.pdf'
  mergePdfFiles(splitFilePaths, mergedFilePath)

  print('\n')
  print(f'Total files retrieved: {str(retrievedFileCount)}')
  print(f'Final joined PDF is at {mergedFilePath}')
