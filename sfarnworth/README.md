# Final machine; sfarnworth sends to kgwynn

Shawn Farnworth's Machine

1. Receives input from laser as Morse Code and converts to ASCII
1. Copies input to a file which is picked up by WebSocket Server daemon process
1. WebSocket server process broadcasts message to all connections, then deletes it's input file

Notes:
* (need note about Laser/Morse code stuff)
* Using PHP 5
* Using PHP-WebSockets project from GitHub

## SETUP

1. Install PHP 5
  1. `sudo apt-get install php5`
1. Add command to '/etc/rc.local' to run on bootup
  1. `sudo vi /etc/rc.local`
  1. Add: `/usr/bin/php /path/to/rgt_3.14/sfarnworth/web-sockets-server/server.php &`
  1. Reboot

