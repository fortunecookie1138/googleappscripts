#!/usr/bin/python

import sys
import requests
import json
import time

apiKey = '18ab7fe66f4a5fcd7a8f06896dbb8c672d781548e44bab1770b182c9924bce59'
endpoint = 'https://' + apiKey + ':@api.hellosign.com/v3/signature_request/list'
headers = {'authorization': "Basic MThhYjdmZTY2ZjRhNWZjZDdhOGYwNjg5NmRiYjhjNjcyZDc4MTU0OGU0NGJhYjE3NzBiMTgyYzk5MjRiY2U1OTo=", 'accept': "application/json"}

class SignerData:
  isComplete = False
  fullName = '' # FullName1
  companyName = '' # Textbox1
  terminal = [] # Textbox2, some people put multiple values in here
  customEmail = '' # Email1
  signatureEmail = '' # from signatures object, COULD be different than their custom input email

  def __init__(self, isComplete, fullName, companyName, terminal, customEmail, signatureEmail):
    self.isComplete = isComplete
    self.fullName = fullName
    self.companyName = companyName
    self.terminal = terminal
    self.customEmail = customEmail
    self.signatureEmail = signatureEmail

def getApiData(page = 1):
  response = requests.get(endpoint + '?page=' + str(page), headers=headers)
  loaded = json.loads(response.text)
  return loaded

firstPage = getApiData()
signatureCount = firstPage["list_info"]["num_results"]
pageCount = firstPage["list_info"]["num_pages"]

# first value of argv is the name of the script being executed
maxPage = int(sys.argv[1]) if len(sys.argv) > 1 else pageCount

signers = []
currentPage = 1
while currentPage <= maxPage:
  print(f'Current page: {str(currentPage)}/{maxPage}')
  pageData = getApiData(currentPage)
  signatures = pageData["signature_requests"]
  for sig in signatures:
    isComplete = sig["is_complete"]
    if len(sig["response_data"]) < 5:
      fullName = sig["response_data"][0]["value"] if bool(isComplete) else ""
      companyName = ""
      terminal = sig["response_data"][2]["value"] if bool(isComplete) else ""
      customEmail = sig["response_data"][3]["value"] if bool(isComplete) else ""
      signatureEmail = sig["signatures"][0]["signer_email_address"]
    else:
      fullName = sig["response_data"][0]["value"] if bool(isComplete) else ""
      companyName = sig["response_data"][2]["value"] if bool(isComplete) else ""
      terminal = sig["response_data"][3]["value"] if bool(isComplete) else ""
      customEmail = sig["response_data"][4]["value"] if bool(isComplete) else ""
      signatureEmail = sig["signatures"][0]["signer_email_address"]

    signer = SignerData(isComplete, fullName, companyName, terminal, customEmail, signatureEmail)

    signers.append(signer)

  currentPage += 1

lines = ['IsComplete,FullName,CompanyName,UserEmail,SignatureEmail,Terminal\n']
i = 0
while i < len(signers):
  line = f'"{signers[i].isComplete}","{signers[i].fullName}","{signers[i].companyName}","{signers[i].customEmail}","{signers[i].signatureEmail}","{signers[i].terminal}"\n'
  lines.append(line)
  i += 1

timestamp = time.strftime("%Y%m%d-%H%M%S")

f = open('C:\src\personalthings\Lyndsey\HelloSignSignatures\\FedexLetterOfConcern_'+timestamp+'.csv', "w")
f.writelines(lines)
f.close()

signedEmails = [s.signatureEmail for s in filter(lambda x: x.isComplete == True, signers)]
uniqueEmails = set(signedEmails)

print('\n')
print(f'Total page count: {str(pageCount)}')
print(f'Total signature count: {str(signatureCount)}')
print(f'Unique signed email count: {str(len(uniqueEmails))}')