#!/usr/bin/python

# HelloSign API docs: https://app.hellosign.com/api/documentation#QuickStart
import sys
import requests
import json
import time

originalPetitionTemplateId = '6405f93f8e93dc84e9f0f01f21fe11ab91c08cb1' # original "FedEx Letter of Concern"
followupPetitionTemplateId = '362bfdc78b64fec14f24971050ff88cab46b1b26' # "Fedex Letter of Concern Follow Up"
apiKey = '18ab7fe66f4a5fcd7a8f06896dbb8c672d781548e44bab1770b182c9924bce59'
endpoint = 'https://' + apiKey + ':@api.hellosign.com/v3/signature_request/list'
headers = {'authorization': "Basic MThhYjdmZTY2ZjRhNWZjZDdhOGYwNjg5NmRiYjhjNjcyZDc4MTU0OGU0NGJhYjE3NzBiMTgyYzk5MjRiY2U1OTo=", 'accept': "application/json"}

# first value of argv is the name of the script being executed
if len(sys.argv) == 1:
  print('Get Signature script options:')
  print('\tFirst arg: Retrieve followup petition stats (0 or 1)')
  print('\tSecond arg: Max number of pages of signatures to retrieve (number, optional)')
  sys.exit()

isFollowUpPetition = int(sys.argv[1]) == 1
maxPage = int(sys.argv[2]) if len(sys.argv) > 2 else 0

templateId = originalPetitionTemplateId if isFollowUpPetition == False else followupPetitionTemplateId
print(f'Is Followup Petition: {str(isFollowUpPetition)}, using templateId: {templateId}')

class SignerData:
  isComplete = False
  fullName = '' # FullName1
  companyName = '' # Textbox1
  terminal = [] # Textbox2, some people put multiple values in here
  customEmail = '' # Email1
  signatureEmail = '' # from signatures object, COULD be different than their custom input email
  userComments = '' # Textbox3, only present in the follow up petition, not the original petition

  def __init__(self, isComplete, fullName, companyName, terminal, customEmail, signatureEmail, userComments):
    self.isComplete = isComplete
    self.fullName = fullName
    self.companyName = companyName
    self.terminal = terminal
    self.customEmail = customEmail
    self.signatureEmail = signatureEmail
    self.userComments = userComments

def getApiData(page = 1):
  response = requests.get(endpoint + '?page=' + str(page) + '&query=template:' + templateId, headers=headers)
  loaded = json.loads(response.text)
  return loaded

firstPage = getApiData()
signatureCount = firstPage["list_info"]["num_results"]
pageCount = firstPage["list_info"]["num_pages"]

maxPage = maxPage if maxPage > 0 else pageCount

signers = []
currentPage = 1
while currentPage <= maxPage:
  print(f'Current page: {str(currentPage)}/{maxPage}')
  pageData = getApiData(currentPage)
  signatures = pageData["signature_requests"]
  for sig in signatures:
    isComplete = sig["is_complete"]
    userComments = ""
    if isFollowUpPetition == True:
      fullName = sig["response_data"][0]["value"] if bool(isComplete) else ""
      companyName = sig["response_data"][2]["value"] if bool(isComplete) else ""
      terminal = sig["response_data"][3]["value"] if bool(isComplete) else ""
      customEmail = sig["response_data"][4]["value"] if bool(isComplete) else ""
      userComments = sig["response_data"][5]["value"].replace('\n', ' ') if bool(isComplete) else ""
      signatureEmail = sig["signatures"][0]["signer_email_address"]
    elif len(sig["response_data"]) < 5:
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

    signer = SignerData(isComplete, fullName, companyName, terminal, customEmail, signatureEmail, userComments)

    signers.append(signer)

  currentPage += 1

lines = ['IsComplete,FullName,CompanyName,UserEmail,SignatureEmail,UserComments,Terminal\n']
i = 0
while i < len(signers):
  line = f'"{signers[i].isComplete}","{signers[i].fullName}","{signers[i].companyName}","{signers[i].customEmail}","{signers[i].signatureEmail}","{signers[i].userComments}","{signers[i].terminal}"\n'
  lines.append(line)
  i += 1

timestamp = time.strftime("%Y%m%d-%H%M%S")

outputFolder = "LetterOfConcern" if isFollowUpPetition == False else "LetterOfConcernFollowUp"
f = open(f'C:\src\personalthings\Lyndsey\HelloSignSignatures\{outputFolder}\FedexLetterOfConcern_{timestamp}.csv', "w")
f.writelines(lines)
f.close()

signedEmails = [s.signatureEmail for s in filter(lambda x: x.isComplete == True, signers)]
uniqueEmails = set(signedEmails)

print('\n')
print(f'Total page count: {str(pageCount)}')
print(f'Total signature count: {str(signatureCount)}')
print(f'Unique signed email count: {str(len(uniqueEmails))}')