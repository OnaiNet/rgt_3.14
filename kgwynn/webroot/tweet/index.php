<?php
/**
 * Tweet input to twitter.
 * Demo from: http://www.pontikis.net/blog/auto_post_on_twitter_with_php
 * Library: codebird-php (bower)
 */

if (empty($_POST)) {
	http_response_code(405); // method not allowed
	exit();
}


//$tweet_suffix = ' #rgt_output'; // now allowing caller to send exactly what it wants (2016-11-21 kag)
$tweet = trim(substr($_POST['tweet'], 0, 140 - strlen($tweet_suffix)));

require_once('../../bower_components/codebird-php/src/codebird.php');

\Codebird\Codebird::setConsumerKey("hY5ApL5WBNKbdOFfH9rzHHpFR", "uxJWUwYWDpJO755LECqubh3rYd2HMzwDcihumqxkwQ4j8SUhZt");
$cb = \Codebird\Codebird::getInstance();
$cb->setToken("796616723885686784-9xxC0vurcpmZ9z7xi0lDfXUltWZhMAZ", "pFskon2hvyqiPU3MN5bdLpRrfyspBssC1Ezq1YvbLGazY");
 
$params = array(
  'status' => $tweet
);
$reply = $cb->statuses_update($params);
?>
