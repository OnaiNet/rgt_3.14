#!/bin/bash

echo $1 > /tmp/test
echo $(whoami) > /tmp/test

convert $1 -colorspace Gray /usr/share/raspb/ocr.tif
tesseract /usr/share/raspb/ocr.tif /usr/share/raspb/result 
#tesseract $1 result
python /usr/share/raspb/sendAnEmail.py
#rm /usr/share/raspb/ocr.tif
#rm /usr/share/raspb/result.txt
