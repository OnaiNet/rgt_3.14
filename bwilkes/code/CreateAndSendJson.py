import sys
import json
import requests

headers = {'Content-type': 'application/json'}


json_string = '{"message": "' + str(sys.argv[1:]) + '"}'
#print(json_string)

json_string = json_string.replace("[","")
#print(json_string)

json_string = json_string.replace("]","")
#print(json_string)

json_string = json_string.replace("'","")
#print(json_string)

print(len(json_string))

r = requests.post("http://67.166.103.221:60916/telephone/", data=json_string, headers=headers)

print(r.status_code)


