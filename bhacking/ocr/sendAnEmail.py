import smtplib
from email.MIMEMultipart import MIMEMultipart
from email.MIMEText import MIMEText
from email.MIMEBase import MIMEBase
from email import encoders


with open('/usr/share/raspb/result.txt', 'r') as myfile:
data="".join(line.rstrip() for line in myfile)

filename = "result.txt"
attachment = open("/usr/share/raspb/result.txt", "rb")

from email.MIMEMultipart import MIMEMultipart
from email.MIMEText import MIMEText

#############Edit these lines#############
fromaddr = "fromemail@gmail.com"
toaddr = "toemail@gmail.com"
############

msg = MIMEMultipart()
msg['From'] = fromaddr
msg['To'] = toaddr
msg['Subject'] = "Pi Test"

body = data
msg.attach(MIMEText(body, 'plain' ))

part = MIMEBase('application', 'octet-stream')
part.set_payload((attachment).read())
encoders.encode_base64(part)
part.add_header('Content-Disposition', "attachment; filename= %s" % filename)

msg.attach(part)

server = smtplib.SMTP('smtp.gmail.com', 587)
server.starttls()
#################Edit this line###############
	server.login(fromaddr, "***password***")
################
text = msg.as_string()
//server.sendmail(fromaddr, toaddr, text)
server.quit()
