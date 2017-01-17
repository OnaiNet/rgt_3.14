import RPi.GPIO as GPIO
import time
import sys

CODE = {'': '',
	' ': ' ',
        "'": '.----.',
        '(': '-.--.-',
        ')': '-.--.-',
        ',': '--..--',
        '-': '-....-',
        '.': '.-.-.-',
        '/': '-..-.',
        '0': '-----',
        '1': '.----',
        '2': '..---',
        '3': '...--',
        '4': '....-',
        '5': '.....',
        '6': '-....',
        '7': '--...',
        '8': '---..',
        '9': '----.',
        ':': '---...',
        ';': '-.-.-.',
        '?': '..--..',
        'A': '.-',
        'B': '-...',
        'C': '-.-.',
        'D': '-..',
        'E': '.',
        'F': '..-.',
        'G': '--.',
        'H': '....',
        'I': '..',
        'J': '.---',
        'K': '-.-',
        'L': '.-..',
        'M': '--',
        'N': '-.',
        'O': '---',
        'P': '.--.',
        'Q': '--.-',
        'R': '.-.',
        'S': '...',
        'T': '-',
        'U': '..-',
        'V': '...-',
        'W': '.--',
        'X': '-..-',
        'Y': '-.--',
        'Z': '--..',
        '_': '..--.-'}

ledPin=18
GPIO.setmode(GPIO.BCM)
GPIO.setup(ledPin,GPIO.OUT)

period = 0.4

def dot():
        sys.stdout.write('.')
        sys.stdout.flush()
	GPIO.output(ledPin,1)
	time.sleep(period)
	GPIO.output(ledPin,0)
	time.sleep(period)

def dash():
	sys.stdout.write('-')
	sys.stdout.flush()
	GPIO.output(ledPin,1)
	time.sleep(3*period)
	GPIO.output(ledPin,0)
	time.sleep(period)

while True:
	input = raw_input('\nWhat would you like to send? ')
	for letter in input:
		symbols = CODE.get(letter.upper(), '..--.-')
		for symbol in symbols:
			try:
				if symbol == '-':
					dash()
				elif symbol == '.':
					dot()
				if (letter.upper() == ' '):
					time.sleep(4*period)
					sys.stdout.write(' ')
					sys.stdout.flush()
			except KeyError:
				sys.stdout.write(' ')
				sys.stdout.flush()
		time.sleep(3*period)
		sys.stdout.write(' ')
		sys.stdout.flush()

