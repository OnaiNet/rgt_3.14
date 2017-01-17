[sourcecode language=”bash”]

#!/bin/bash

echo "Recording Now"

arecord -D "plughw:1,0" -f cd -t wav -d "10" -c 1 -r 16000 | flac - -f --best --sample-rate 16000 -o out.flac

echo "Changing to a .wav file"
sox out.flac out.wav


#aplay out.wav

echo "Translating the audio file using Google Cloud Speech API"
sudo python Translate_File.py out.wav

value=$(<message_out.txt)


sudo python CreateAndSendJson.py "$value"

#sleep 5

rm out.flac

echo "All Done"