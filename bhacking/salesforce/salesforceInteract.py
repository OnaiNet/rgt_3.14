from simple_salesforce import Salesforce

sf = Salesforce(username='myemail@gmail.com', password = 'password', security_token='tokenGoesHere', sandbox=False)

sf.PiStuff__c.create({'Message__c':'This is a test'});
