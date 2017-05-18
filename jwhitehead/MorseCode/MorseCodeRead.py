import RPi.GPIO as GPIO
import time
import sys
from datetime import datetime

morseAlphabet = {'': '',
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
	'_': '..--.-'
        }

inverseMorseAlphabet=dict((v,k) for (k,v) in morseAlphabet.items())

filename = "/home/pi/rgt_3.14/sfarnworth/web-sockets-server/output" 
getMessage = False

dot = 450
word = 5*dot
  
GPIO.setmode(GPIO.BCM)
count = 0
tstart = datetime.now()
tend = datetime.now()


morseLetter = ''

# GPIO 17 set up as inputs, pulled up to avoid false detection.  
# Ports is wired to connect to GND on button press.  
# So we'll be setting up falling edge detection

GPIO.setup(17, GPIO.IN, pull_up_down=GPIO.PUD_UP)

# now we'll define threaded callback functions  
# it will run in another thread when our events are detected  
def my_callback(channel):
    global count
    global tstart
    global tend
    global morseLetter
    global getMessage
    global message
    global filename
  
    if GPIO.input(17):
	tstart = datetime.now()
        delta = tstart-tend
	milliseconds = int(delta.total_seconds()*1000)

        if milliseconds > dot:
		try:
			letter = inverseMorseAlphabet[morseLetter]
		except KeyError, e:
			letter = '_'
		if getMessage:
			sys.stdout.write(letter)
			message = message + letter
		if letter == '{':
			sys.stdout.write("\nBegin message\n")
                        getMessage = True
			message = ""
		morseLetter = ''		
		#sys.stdout.write(' ')
		sys.stdout.flush()	
	if milliseconds > word:
		message = message + ' '
		sys.stdout.write(' ')
		sys.stdout.flush()
#    	print "Rising Edge detected on 17:", count
    else:
	tend = datetime.now()
	delta = datetime.now()-tstart
        milliseconds = int(delta.total_seconds() * 1000)
#	print "Falling Edge detected on 17:", count, milliseconds,
	count = count + 1  
    	if milliseconds > dot:
		morseLetter += '-'
		sys.stdout.write('-')
		sys.stdout.flush()
	else:
		morseLetter += '.'
		sys.stdout.write('.')
		sys.stdout.flush()

#raw_input("Press Enter when ready\n>")  
  
# when a falling edge is detected on port 17, regardless of whatever   
# else is happening in the program, the function my_callback will be run  
GPIO.add_event_detect(17, GPIO.BOTH, callback=my_callback, bouncetime=300)

while True:
	time.sleep(1)
        delta = datetime.now()-tend
	#print delta.total_seconds()
	#sys.stdout.flush
        milliseconds = int(delta.total_seconds()*1000)
        if milliseconds > (10*dot):
		letter = inverseMorseAlphabet.get(morseLetter, ' ')
		if letter == '}':
			sys.stdout.write("\nEnd message\n")
                        sys.stdout.write("\nReceived message: ")
			sys.stdout.write(message)
			getMessage = False
			sys.stdout.write('\n')
                	morseLetter = ''
			sys.stdout.write('\nWrite output file: ')
			sys.stdout.write(filename)
			sys.stdout.write('\n')
                	
			sys.stdout.flush()
						
			target = open(filename, 'w')
			target.truncate()
		        target.write(message)
			target.close()
			#sys.exit(0)

