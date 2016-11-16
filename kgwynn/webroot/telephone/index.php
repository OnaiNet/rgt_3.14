<?php
/**
 * Converts input text as if in the telephone game, changing some words to words that sound similar, using Metaphone
 *
 * @author Kevin Gwynn <kevin.gwynn@gmail.com>
 * @since 2016-11-15
 */

define('WORDS_FILE', file_exists('/usr/dict/words') ? '/usr/dict/words' : '/usr/share/dict/words');
define('DEBUG', !empty($_GET['debug']));

$input = trim($_GET['input']);

if (empty($input)) {
	http_response_code(400);
	exit(0);
}

echo telephone_convert($input);

function telephone_convert($input) {
	$length = strlen($input);
	$start_word = $length % 4;
	$nth_word = $length % 6 + 4;

if(DEBUG)echo "length = [$length]<br>start_word = [$start_word]<br>nth_word = [$nth_word]<br>";

	$words = explode(' ', $input);	

	for ($i = $start_word; $i < sizeof($words); $i += $nth_word) {
		$words[$i] = find_word_sounds_like($words[$i]);
	}

	return implode(' ', $words);
}

function find_word_sounds_like($word) {
	$original_word = $word;
	$word = preg_replace("/[^\w]/", '', $word);
	$word_converted = metaphone($word);
	$matches = array();

	$dict = fopen(WORDS_FILE, 'r');
	
	while($new_word = strtolower(trim(fgets($dict, 255)))) {
		if ($new_word == strtolower($word)) {
			continue;
		}

		$new_word_converted = metaphone($new_word);
		
		if ($new_word_converted == $word_converted) {
			$matches[] = $new_word;
		}
	}
if(DEBUG)echo "word = [$word]; matches = ";
if(DEBUG)print_r($matches);

	if (sizeof($matches) == 0) return $original_word;
	//shuffle($matches); // This is not idempotent!
	$pick = sizeof($matches % 3);
	if ($pick >= sizeof($matches)) $pick = sizeof($matches) - 1;

	return $matches[$pick];
}

?>
