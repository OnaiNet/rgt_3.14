#!/usr/bin/env python

import pika
import RPi.GPIO as GPIO
import time
import sys
import json

CODE = {'': '',
	'{': '-.-.-',
	'}': '.-.-.',
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


credentials = pika.PlainCredentials('rgt_314','chg@Test')
connection = pika.BlockingConnection(pika.ConnectionParameters(host='67.166.103.221',
        credentials=credentials))

channel = connection.channel()

channel.queue_declare(queue='rgtQueue')

def callback(ch, method, properties, body):
        print(" [x] Received %r" % body)

	json_data = '{ ' + body + ' }'
 	print(" [x] json_data %r" % json_data)

	data = json.loads(json_data)
	input = data['message']
	input = '{'+input+'}'

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

channel.basic_consume(callback,
        queue='rgtQueue',
        no_ack=True)

print(' [*] Waiting for messages. To exit press CTRL+C')

channel.start_consuming()

