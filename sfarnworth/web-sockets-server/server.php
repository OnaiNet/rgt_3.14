#!/usr/bin/env php
<?php
/**
 * Modified from testwebsock.php example
 * Full web socket server for RGT 3.14 project
 *
 * @see https://github.com/ghedipunk/PHP-Websockets
 * @author Kevin Gwynn <kevin.gwynn@gmail.com>
 */

chdir(dirname(__FILE__));
require_once('./websockets.php');

class RGTWebSocketServer extends WebSocketServer {
  //protected $maxBufferSize = 1048576; //1MB... overkill for an echo server, but potentially plausible for other applications.

  protected function process ($user, $message) {
    $this->send($user,$message);
  }

  protected function connected ($user) {
    // Do nothing: This is just an echo server, there's no need to track the user.
    // However, if we did care about the users, we would probably have a cookie to
    // parse at this step, would be looking them up in permanent storage, etc.
  }

  protected function closed ($user) {
    // Do nothing: This is where cleanup would go, in case the user had any sort of
    // open files or other objects associated with them.  This runs after the socket
    // has been closed, so there is no need to clean up the socket itself here.
  }

  /**
   * On each tick, check for 'output' file and if found, broadcast its contents to each connected user, then delete it
   */
  protected function tick() {
    // Check for output file, if it doesn't exist, do nothing
    if (!file_exists('output')) {
      return;
    }

    // Get file contents as string
    $output = file_get_contents('output');
    
    // Delete file
    unlink('output');

    // broadcast output file contents to all users
    foreach ($this->users as $user) {
      $this->send($user, $output);
    }
  }
}

$echo = new RGTWebSocketServer("0.0.0.0","9000");

try {
  $echo->run();
}
catch (Exception $e) {
  $echo->stdout($e->getMessage());
}
