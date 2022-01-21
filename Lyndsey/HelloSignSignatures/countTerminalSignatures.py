#!/usr/bin/python

import time

# assumes I've stripped the input files down to one number per line
allTerminalsPath = 'C:\src\personalthings\Lyndsey\HelloSignSignatures\AllTerminalNumbers_1-20-22.txt'
signedTerminalsPath = 'C:\src\personalthings\Lyndsey\HelloSignSignatures\SignedTerminals_1-20-22.txt'
outputPath = 'C:\src\personalthings\Lyndsey\HelloSignSignatures'

def readFile(path):
  with open(path) as f:
    return f.readlines()

allTerminals = readFile(allTerminalsPath)
signedTerminals = readFile(signedTerminalsPath)

counts = {}
for terminal in allTerminals:
  index = terminal.strip().lstrip('0')
  counts[index] = 0

print(counts)

invalidTerminals = []
for terminal in signedTerminals:
  index = terminal.strip().lstrip('0')
  if index in counts:
    print(index)
    counts[index] += 1
  else:
    invalidTerminals.append(index)

print('Counted terminals:')
print(counts)
print('Invalid terminals:')
print(invalidTerminals)

lines = ['Terminal,Count\n']
i = 0
for key in counts:
  lines.append(key + ',' + str(counts[key]) + '\n')

timestamp = time.strftime('%Y%m%d-%H%M%S')
f = open(outputPath + '\\SignedTerminalCounts_'+timestamp+'.csv', 'w')
f.writelines(lines)
f.close()
