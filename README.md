# Rube-Goldberg Telephone Pi (RGT 3.14)

Rube-Goldberg Telephone Pi (aka RGT 3.14) - A silly project, just for fun and learning at CHG.

[First, watch this video on YouTube.](https://youtu.be/qybUFnY7Y8w)

### Table of Contents
1. [What is RGT 3.14?](#what-is-rgt-314)
2. [Raspberry Pi Notes](#raspberry-pi-notes)
3. [Getting Started](#getting-started)

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

## Getting Started
