# Python is case sensitive for variable and function names.
# imports
from os import system

# Create the image file
def create_image( textFileName="TelephonePiMessage.txt", imageFileName = "TelephonePiImage.png"):
    system( "convert -size 1675x990 xc:transparent -font Arial -pointsize 48 -channel RGBA -gaussian 0x6 -fill black -draw \"text 20,320 'Hola mondo'\" myfile.png")
    
    print 'CreateImage'

# Display the image file.
def display_image( imageFileName = "TelephonePiImage.png"):
    print "displayImage " + imageFileName

# Get the message from Twitter.
def get_twitter_message( feed):
    print 'get twitter message ' + feed
    message = 'Four score and seven years ago our forefathers...'
    return message

# Change understanding of the message
def understanding( message):
    pass

# Capture image
def capture_image():
    # webservice call to "http://50.96.223.29:8080/0/action/snapshot"
    pass

#=======================
textfile = 'PiFile.txt'
imagefile = 'PiImage.png'
feedname = 'tfeed'

message = get_twitter_message( feedname)
understanding( message)
create_image( textfile, imagefile)
display_image( imagefile)

system( 'rmIfFile.txt')

cmd = 'printf "' + message + '" > ' + textfile
print cmd
system( cmd)
