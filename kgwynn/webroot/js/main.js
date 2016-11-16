
var $input = $('INPUT');
var output = '';
var $output = $('#output');
var $window = $(window);
var $body = $('BODY');

var onWindowResize = function() {
	$input.css('width', $window.width());
	$input.css('left', $window.width() / 2 - $input.width() / 2 + 'px');
	$input.css('top', $window.height() / 2 - $input.height() / 2 + 'px');
};

var onKeydown = function(e) {
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
	console.log('Now, animate the input!');
	$output.addClass('animating');
	$output.text(output);
	setTimeout(function() { $output.removeClass('animating'); tweet(output); }, 4500);
};

var tweet = function(tweet) {
	$.ajax('tweet/', {
		data: {
			tweet: tweet
		},
		method: 'POST',
		complete: function(response) {
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
			console.log(response);
			var result = response.responseText;
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

$(document).ready(function(){
	$window.on('resize', onWindowResize);
	$input.on('keydown', onKeydown);//.on('blur', function() { this.focus(); });
	onWindowResize();
	$input.focus();
});

