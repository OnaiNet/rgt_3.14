# Machine: kgwynn; machine 0

Kevin Gwynn's demo machine

1. Takes initial input from keyboard
1. Tweets initial input: twitter.com/#rgt_output
  1. Machine 1 will listen for tweet as its input
1. Waits for input from last machine (currently Paul's Pi, via Web Sockets)
1. Displays final result on screen

Other notes:
* Using PHP w/ Apache HTTPD webserver
* Using Codebird-PHP Twitter library (via Bower)
* Using jQuery and CSS3 Transitions / Animations for UI

## SETUP

Do the following from within this 'kgwynn' directory:

1. Install American dictionary
  1. `sudo apt-get install wamerican`
1. Install PHP
  1. `sudo apt-get install php5` (double-check this)
1. Install NPM
  1. `sudo apt-get install npm`
1. Install Bower
  1. `sudo npm install -g bower`
1. Install all Bower components
  1. `bower install` (this installs Codebird PHP, a Twitter library)
1. Turn on web-server
  1. `cd webroot`
  1. `php -S localhost:8080`
1. Access by going to 'http://localhost:8080' or by using IP or a known hostname

## Considerations

I'm considering adding an AMQP listener in the main front-end client to listen to a queue of messages to which we could log events to track the process through. Here's a possible library: https://github.com/dansimpson/amqp-js

If Steve's Pi was set up with an endpoint that would just queue these messages, it would involve a simple cURL request on each machine when it receives input and when it sends output.
