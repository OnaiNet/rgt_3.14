# Rube-Goldberg Telephone Pi (RGT 3.14)

Rube-Goldberg Telephone Pi (aka RGT 3.14) - A silly project, just for fun and learning at CHG.

[First, watch this video on YouTube.](https://youtu.be/qybUFnY7Y8w)

### Table of Contents
1. [What is RGT 3.14?](#what-is-rgt-314)
2. [Raspberry Pi Notes](#raspberry-pi-notes)
3. [Getting Started](#getting-started)
4. [RGT Ideas](#rgt-ideas)

## What is RGT 3.14?

**The Project Goal: Create a virtual Rube-Goldberg-esque telephone game of inputs and outputs between a series of machines**

Given there are 10 Raspberry Pi machines, Machine 0 through Machine 9.

Machine 0 will take input from a human in some way. Machine 0 passes the input to Machine 1. Machine 1 receives the input and translates it in some way then passes it to Machine 2. Eventually, Machine 9 will pass its output to Machine 0, which will then display the final result. Each machine should visually indicate, in some way, that it is receiving input, processing the input, and sending output.

The goal is to have a visual representation of data being transferred and translated between 10 machines, that a human can watch with some entertainment value, and hopefully the final response being somewhat similar to the original input, but hopefully at least a little humorous.

### Requirements

1. Machine<sub>n</sub> must receive input from Machine<sub>n-1</sub>.
2. Machine<sub>n</sub> must translate/modify the text in some way. Ideally--but not strictly required--, this modification will be *[idempotent](https://en.wikipedia.org/wiki/Idempotence)*: given the same input, the output will always be consistent.
3. Machine<sub>n</sub> should provide some visual indication that it is receiving and/or processing and/or transmitting.
4. Machine<sub>n</sub> must send its output to Machine<sub>n+1</sub>.
  1. Each transport method between machines must be a unique method!
  2. If machine 3 decides to send to machine 4 via FTP, no other machine can use FTP as its transport method.

**OPTIONAL** Once the requirements are met, spice things up by making your visual output fancier, or improve logging. Some additional ideas:

1. Make your visual output/display very fancy.
  1. Maybe utilize a breadboard and the Pi's GPIO pinout to do some fancy visual or audio indicators.
  2. 3-D transforms? Animation? Go wild.
2. Log all your inputs and outputs in a persistent store.
  1. Provide some interface where a history of your machine's inputs and outputs can be reviewed.

### Assumptions

1. Developer<sub>n</sub> must work with developer<sub>n-1</sub> to determine how he/she will recieve input, and must work with developer<sub>n+1</sub> to determine how he/she will send output.
2. Developers can work in any way they please, as long as they don't impede the developers on either side, unreasonably.
3. Developer<sub>n-1</sub> ultimately gets to choose his/her transport method for sending to machine<sub>n</sub>, but should at least consult with developer<sub>n</sub> to make sure the transport won't be too difficult to receive. Put people first!
4. Developers will be expected to help any non-developers so we can all learn.
5. By default, all the Pi's will have Raspbian installed, which is a Debian-based Linux OS. We'll have to figure out our way around in this environment.
6. The project sponsors are very willing to help along the way! (contact Kevin Gwynn or Steve Keiser)

### Demo

If you missed the demo in person, here's essentially what happened:

1. Machine 0 (Kevin's Pi) is hooked up to a monitor. On the screen is a browser window at "http://kgwynn.rgt". There is a single cursor blinking in the middle of the screen.
2. Someone suggests an input and it is typed into the box.
3. Once the ENTER key is hit, the text is erased, then animates across the screen.
4. Meanwhile the Machine 0 tweets the input: https://twitter.com/hashtag/rgt_output
5. Machine 1 (Paul's Pi) is in the room, but with no monitor or keyboard/mouse. It does have a breadboard attached and has an LED, an RGB, and an audio beep-speaker.
6. Machine 1 receives the input, translates it from one language to another, to another, to another, and back to English.
6. While translating, Machine 1 indicates it is receiving, processing, and sending a message via lights and beeps.
7. Machine 1, via WebSockets, sends its results back to Machine 0.
8. Machine 0 takes the results and does its own translation, converting a few words (if any) to words that SOUND the same when spoken.
9. Machine 0 displays the final results on the screen.
10. Machine 0 also tweets the result: https://twitter.com/hashtag/rgt_results

## Raspberry Pi Notes
There are lots of guides and information out there about the Raspberry Pi. Start by checking out the Raspberry Pi Foundation's website:
https://www.raspberrypi.org/help/

One thing we have done to save time is we pre-loaded the SD cards with the Raspbian Jessie Operating System with Pixel (desktop). This is a Debian-based ARM-architecture lightweight Linux distribution.

If you want to install a different OS, you're welcome to do so, but you'll have less group support.

## Getting Started
### Hardware

1. Get all your parts out and make sure you're not missing something. You should have:
  1. Raspberry Pi
  2. Small package of two heat sinks
  3. A MicroSD card
  4. A clear case for your Pi
  5. A power cord
2. IF NEEDED: A DVI-to-HDMI adapter (CHG is LENDING these to us; please plan to return it) -- let me know if you need one
3. Put on your heat sinks - they have sticky-back adhesive
4. Put your Pi in its case
5. Plug in your pre-formatted MicroSD card
6. Plug in your display, a keyboard and mouse, then plug in the power
  1. *NOTE:* The Pi does NOT have a power button. You power it on by plugging it in. You can power it off using software shutdown, but you will have to unplug/plug it back in to power it on. This is why you should plug your display and peripherals in before plugging in the power.

### Software
Once you boot up, there are a few configuration things you'll want to do. You'll want to get connected so you can update your software. Plug into a network via Ethernet, or connect via Wifi (there is a Wifi icon in the upper-right hand corner in Pixel that can guide you through connecting to MOST networks).

#### Connect to CHG's Wifi
CHG's Wifi is is a WPA2-Enterprise network and is not handled by Pixel's default GUI-based Wifi connector. To connect:

1. `sudo vi /etc/wpa_supplicant/wpa_supplicant.conf` (edit this file with whichever editor you prefer)
1. Refer to the example [WPA supplicant config file](setup/wpa_supplicant.conf)
1. Save the file, exit to the terminal
1. `sudo wpa_cli status` (confirm your Wifi adapter is on, but INACTIVE)
1. `sudo wpa_cli reconfigure` (tells the Wireless adapter to re-load its configuration)
1. `sudo wpa_cli reconnect`
1. `sudo wpa_cli status` (this should show AUTHENTICATED or something else to indicate it is connected/connecting. You run this command multiple times to see the status update)
1. `ifconfig` (this is similar to `ipconfig` in Windows; shows each network adapter and its status. "wlan0" is the Wireless adapter. Ideally, we'd like to see a '10.10.x.x' address here once connected.)

#### Connect to CHG's VPN
For now, you might have to figure out how to do this on your own if you plan on working with your Pi remotely.

#### First time setup
Once you are connected:

1. `sudo apt-get update && sudo apt-get upgrade` (this will update apt-get and upgrade your OS)
  1. You may need to reboot your Pi after this
2. From the main menu: Preferences -> Raspberry Pi Configuration **or** `sudo raspi-config` (this runs the Raspberry Pi configuration program)
  1. Set your timezone
  2. Set your locale (by default everything is set up for the UK)
  3. Set your language as US-English
  4. Set your keyboard layout

### Programming languages
Raspbian comes with:

1. Perl (5.20)
2. Java (1.8)
3. Nodejs (0.10)
4. Python (2.7.9)

You can install pretty much anything. I used PHP (5.6) for my demo. Paul used C# with Mono for his demo.

### Editors
Raspbian comes with:

* Command-line editors
  1. Nano
  2. VIM (VI-IMproved)
* Window-based/GUI editors
  1. Geany
  2. Some others?

VIM is superior. No battle. It does have a steep learning curve though.

Most people find Nano simpler, however. I'll help you with VIM if you want, someone else will help you with Nano.

Geany seems like a strong option if you are using the Pi connected to a monitor in the desktop environment.

You can install a plethora of other editors such as Visual Studio Code (VSCode) or Emacs.

### Git
Rasbpian comes with Git installed. We are using GitHub, so that's good. You'll need to use Git to clone this repository. You will store all your code in this project so we can all share.
  
## RGT Ideas

### Possible text translations:
1. SoundEX fuzzy matching (perfect for "Telephone" game)
  1. Find similar sounding words
  1. Convert to phonetic version
2. Levenshtein Distance - Swap a word or two with close distance
  1. PHP algorithm
3. Metaphone algorithm
4. Synonyms of random words
  1. http://thesaurus.altervista.org/
5. Swap words - maybe nouns only?
  1. Would need to be able to identify nouns, verbs (Natural language processing)
  1. Stanford NLP Group
  1. Stanford Parser
6. Convert to another language *
7. Convert back to English *
8. Change phrase tense?
  1. Would need to be able to determine and convert tense
9. Add punctuation
10. Add random adverbs or adjectives
  1. Must be able to identify nouns, verbs
11. Dialect conversion - 
  1. See for ideas: http://rinkworks.com/dialect/
  1. Snoop Dogg ala Gizoogle (Warning - offensive language)
    1. http://www.gizoogle.net/
    1. Noun: Drop last vowel+consonant, add izzle
      1. Dog = dizzle
      1. Hamilton = Hamiltizzle
  1. Redneck
  1. LA-girl (add random 'like's)
12. Replace word-parts with other random word-parts from dictionary
  1. Replace a noun with another random noun
13. Convert case - lower, upper, mixed, random

\* - Could be re-used/multiple times

### Possible Visual Transitions:
1. Zoom-in/out
2. Visually typed out
3. Spinning, growing, etc.
4. Show text flowing into a "machine" and getting spit out
  1. Conveyor belt
5. "Typewriter" style output
  1. Possibly with sound, paper-look background
6. 3-D effects
7. "The Matrix"
8. GPIO - LED's, RGB's, beeps, etc.

### Possible Data Transport Methods:
1. WebService call (SOAP/REST)
2. HTTP Form POST / GET
3. Pub/Sub MessageQueue (e.g. RabbitMQ)
4. Websockets
5. Twitter publish/read *
6. Write to floppy disk
  1. Someone physically moves floppy disk *
7. Other hardware?? Arduino? Switches? Actual RG-type things??
  1. Actuator pushes ball down ramp, hits button?... (but this isn't transmitting text…)
  1. BUT…. This could tell the system when to read from a floppy or something like that
8. Print out text -> Scan + OCR on the receiving side
9. Text-to-image -> OCR on Image
10. Text-to-speech -> Speech-to-text
11. Read screen via camera + OCR
12. FTP *
13. SSH
14. SCP *
15. rsync *
16. NTFS *
17. Email *
18. Azure bus *
19. UDP sockets
20. TCP direct connection
21. netcat
22. Microsoft Flow
