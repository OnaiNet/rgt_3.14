# Tools for RGT 3.14 Development

## getip
Gets your current WLAN IP address and outputs it.
```
$ getip
192.168.1.154
```

Use this tool to export your WLAN IP address to the "ip" file in your machine's project folder:
```
$ cd rgt_3.14/kgwynn
$ ../tools/getip > ip
$ git add ip
$ git commit -m "Updated IP address for kgwynn" && git push
```

