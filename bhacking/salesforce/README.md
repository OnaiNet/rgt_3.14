So, I decided to try connecting my Pi to Salesforce and found
a tutorial to do this at https://www.youtube.com/watch?v=JCL0cK4IZzw
The tutorial was designed to set up an infrared sensor to track when rooms
at an office are occupied.  I removed the room tracking pieces as I only
wanted to know how to insert/update records from a Pi.  Here are the steps.

Setup
  Run the following on your command line.
    sudo apt-get install python3-pip
    sudo pip install simple-salesforce

Script
  You need to include the following in a python script.
    This line imports what you just installed into your python script.
      from simple_salesforce import Salesforce

    Next, establish your connection. (sandbox can be true or false depending on what environment you are connecting with)
      sf=Salesforce(username='me@chg.com', password='myPassword', security_token='BigUglyToken', sandbox=False)

    After this, you can conect and modify your fields as needed.
      sf.PiStuff__c.insert({'Message__c':'Message Text goes here'})
    or
      sf.PiStuff__c.update('[recordId]', {'Message__c':'New Message Text goes here'})
      

And that's about it.  Hope you find this useful.  You can check out simple-salesforce if you want to access other options such as upsert, delete, etc.

Also, to see the full script, check out salesforceInteract.py to see the full script. 
