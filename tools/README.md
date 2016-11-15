# Tools for RGT 3.14 Development

## getip
Gets your current WLAN IP address and outputs it.
```
$ getip
192.168.1.154
```

Use this tool to export your WLAN IP address to the "ip" file in your machine's project folder:
```
$ cd rgt_3.14
$ tools/getip > kgwynn/ip
$ git add kgwynn/ip
$ git commit -m "Updated IP address for kgwynn" && git push
```
## sethosts
Sets the current list of IP's to hostnames in your hosts file. This tool must be used with sudo/root access:
```
$ cd rgt_3.1.4
$ sudo tools/sethosts
```
This creates a copy of your original hosts file and saves it to /etc/hosts.orig . Your /etc/hosts file will now contain one entry for each folder of the RGT project which contains an 'ip' file. The hostname for each entry will be "rgt_" followed by the folder name. Example:

```
kgwynn@SURFACE:/mnt/c/dev/pi/rgt_3.14$ find . -name "ip"
./kgwynn/ip
kgwynn@SURFACE:/mnt/c/dev/pi/rgt_3.14$ tail -n 3 /etc/hosts

# RGT_3.14 sethosts (20161114-1742):
10.41.127.198 rgt_kgwynn
```

