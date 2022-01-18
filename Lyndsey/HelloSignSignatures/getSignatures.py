#!/usr/bin/python

import requests
import json
import csv
import time

apiKey = '18ab7fe66f4a5fcd7a8f06896dbb8c672d781548e44bab1770b182c9924bce59'
endpoint = 'https://' + apiKey + ':@api.hellosign.com/v3/signature_request/list'
headers = {'authorization': "Basic MThhYjdmZTY2ZjRhNWZjZDdhOGYwNjg5NmRiYjhjNjcyZDc4MTU0OGU0NGJhYjE3NzBiMTgyYzk5MjRiY2U1OTo=", 'accept': "application/json"}

def getApiData(page = 1):
  response = requests.get(endpoint + '?page=' + str(page), headers=headers)
  loaded = json.loads(response.text)
  return loaded

firstPage = getApiData()

signatureCount = firstPage["list_info"]["num_results"]
print('Signature count: ' + str(signatureCount))

pageCount = firstPage["list_info"]["num_pages"]
print('Page count: ' + str(pageCount))

emails = []
isComplete = []
currentPage = 1
# while currentPage < 5: # for testing with only a few API calls
while currentPage < pageCount:
  print('Current page: ' + str(currentPage))
  pageData = getApiData(currentPage)
  signatures = pageData["signature_requests"]
  for sig in signatures:
    email = sig["signatures"][0]["signer_email_address"]
    status = sig["is_complete"]
    print(email + ' is ' + str(status))
    emails.append(email)
    isComplete.append(status)

  currentPage += 1

# print(emails)

lines = ['Email,IsComplete\n']
i = 0
while i < len(emails):
  lines.append(emails[i] + ',' + str(isComplete[i]) + '\n')
  i += 1

timestamp = time.strftime("%Y%m%d-%H%M%S")

f = open('C:\src\personalthings\Lyndsey\HelloSignSignatures\\FedexLetterOfConcern_'+timestamp+'.csv', "w")
f.writelines(lines)
f.close()