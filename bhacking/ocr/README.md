Here is the process I've worked through to set up image capture on my machine
as well as OCR to translate the image into text.

The plan:  
1.  The sending computer displays the text it want's to send on it's screen.  
2.  A desktop or laptop computer connects to the sending computer using VNC 
    a. (Google hangouts was too resource intensive for the computer sharing data and caused my pi to crash when I was testing).  
3.  The receiving computer aims a camera at the desktop/laptop computer displaying the result from the VNC screenshare.  
4.  The sending computer makes a http request to the receiving computer.
5.  All steps from here are handed by the receiving computer (pi)
6.  Take a snapshot of the screen
7.  Motion saves the snapshot and calls ocr.sh
8.  ocr.sh converts converts the image to grayscale and saves it as a .tif file.
9.  ocr.sh uses tesseract to convert the .tif file to text and saves it to result.txt
9.  ocr.sh calls sendAnEmail.py
10. sendAnEmail.py takes result.txt and attaches it to an email which is sent via smtp.

Software I've installed.  You can run these as a single command, but I've split
them out to make them easier to read.

  sudo apt-get install imagemagick      //I actually haven't used this other than to view files.  
					//I had other plans for it, but it ended up not being needed.

  sudo apt-get install libpng12-dev     //png images

  sudo apt-get install libjpeg-dev      //jpg images

  sudo apt-get install libtiff5-dev     //tif images

  sudo apt-get install tesseract        //This is what I ended up using to convert the image to text

  sudo apt-get install motion           //This allows for motion detection using my camera and can take 
					//pictures or video based on number of pixels changed, or it can 
					//use an http request to trigger taking a picture.

This was installed on my laptop, not the pi

  VNC                                   //This was used for displaying the data from the sending pi.

The process:

1.  Installed VNC.  I installed VNC on my laptop so that I could connect to the sending computer and view what it had on 
    its screen.  I had to get an IP address, username and password to connect to his computer.  Once I was able to 
    connect, I left my screen in full page mode viewing whatever was displayed on the pi.  I used full screen to avoid
    having any extra lettering from open tabs that could possibly be picked up by the viewer.

2.  I installed the apt-get programs listed above on my pi.  I then opened motion.conf and modified various settings 
    The settings I changed are as follows:

	width			//Optimize camera
	height			//Optimize camera
	threshold		//Set this to 5000000 to turn off motion activated camera activity.
	pre_capture		//I only wanted one photo at a time
	post_capture 		//I only wanted one photo at a time
	target_dir		//Set the default directory where to save video and photos
	snapshot_filename	//Set the location and name where snapshots were to be saved
	picture_filename      	//Set the location and name where pictures were to be saved
	movie_filename       	//Set the location and name where movies were to be saved
	webcontrol_localhost	//Turn this off to allow outside users to trigger snapshots
	on_picture_save		//Specifies the file to run when an image is captured and includes the image as a parameter
	

3.  I next had to expose port 8080 on my modem to allow access to my pi.  I did this by modifying the NAT setting.  You 
    can also set your pi into the demilitarized zone on your modem, but since this is done differently on each network,
    I'm not going to include the steps.  I had to do this because I work remotely and my pi was not on the internal network.

4.  ocr.sh handles the image conversion to text and calls sendAnEmail.py. I don't think there is much to explain here.

5.  sendAnEmail.py handles sending the email attachment.  I followed the advanced email sending instruction found 
    here...naelshiab.com/tutorial-send-email-python.  I had to set 'allow less secure apps on your account' on the 
    gmail account which caused headaches for a bit.

6.  And... done!
