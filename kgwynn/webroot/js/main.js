
var $input = $('INPUT');
var output = '';
var $output = $('#output');
var result = '';
var $result = $('#result');
var $window = $(window);
var $body = $('BODY');
var connection;  // WebSocket connection to listen for Result

var onWindowResize = function() {
	$input.css('width', $window.width());
	$input.css('left', $window.width() / 2 - $input.width() / 2 + 'px');
	$input.css('top', $window.height() / 2 - $input.height() / 2 + 'px');
};

var onKeydown = function(e) {
	$result.html(''); // clear out the result, just in case.

	if (e.keyCode == 13) {
		if (!validate($input.val())) {
			onError();
		} else {
			submitOutput($input.val());
		}
	}	
};

var validate = function(input) {
	if (input.length == 0) return false;

	return true;
};

var submitOutput = function(input) {
	output = input;
	$input.addClass('transmitting');
	console.log('Visually clearning input box...');
	clearInput();
};

var clearInput = function() {
	var input = $input.val();
	if (input.length == 0) {
		animateInput();
		return;
	}
	$input.val(input.slice(0, -1));
	var delay = Math.random() * 40 + 10;
	setTimeout(clearInput, delay);
};

var animateInput = function() {
	$input.removeClass('transmitting');
	console.log('Animating the input...');
	$output.addClass('animating');
	$output.text(output);
	setTimeout(function() { 
		$output.removeClass('animating');
		tweet(output + ' #rgt_output');
		broadcast(output);
	}, 8500);
};

/**
 * Send output via WebSocket connection
 */
var broadcast = function(message) {
	console.log('Broadcast requested to WebSocket: ' + message);
	connection.send('broadcast ' + message);
};

/**
 * Tweet output
 */
var tweet = function(tweet) {
	console.log('Tweet!: ' + tweet);
	$.ajax('tweet/', {
		data: {
			tweet: tweet
		},
		method: 'POST',
		complete: function(response) {
			console.log('Tweet response:');
			console.log(response);
		}
	});
};

var telephone = function(input) {
	$.ajax('telephone/', {
		data: {
			input: input
		},
		complete: function(response) {
			result = response.responseText;
			$result.html(result).addClass('animating');
			//setTimeout(function() { $result.removeClass('animating'); }, 8500);
			console.log('Telephone: ' + result);
		}
	});
};

var onError = function() {
	$input.addClass('error');
	setTimeout(function() {
		$input.removeClass('error');
	}, 1500);
};

var processFinalMessage = function(message) {
	console.log('Received final message: ' + message);
	// Remove/stop animation if has already happened/started
	$result.removeClass('animating');
	telephone(message);
	tweet(message + ' #rgt_results');
};

$(document).ready(function(){
	$window.on('resize', onWindowResize);
	$input.on('keydown', onKeydown);//.on('blur', function() { this.focus(); });
	onWindowResize();
	$body.on('click', function() { $input.focus(); });
	$input.focus();

	// Open socket to listen for result
	connection = new WebSocket('ws://pcarver.rgt:8005');
	
	connection.onopen = function() {
		console.log('WebSocket opened; listening for result');
	};

	connection.onclose = function() {
		console.log('WebSocket closed');
	};

	connection.onmessage = function(message) {
		console.log('WebSocket Message Received: ' + message.data);
		processFinalMessage(message.data);
	};
});

