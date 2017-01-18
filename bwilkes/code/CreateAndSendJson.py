import sys
import json
import requests

json_string = '{"message":' + str(sys.argv[1:]) + '}'

json_string = json_string.replace("[","")

json_string = json_string.replace("]","")

json_string = json_string.replace('"',"'")

print(json_string)

#r = requests.post("http://localhost:8080", json=json_string)

#print(str(sys.argv[1:]))
#json_data = simplejson.dumps(json_string)
#payload = {'json_payload': json_data}
#r = requests.post("http://localhost:8080", data=payload)

