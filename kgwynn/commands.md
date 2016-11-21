    1  pwd
    2  cd .
    3  ll
    4  cd ..
    5  ll
    6  ls -Fla
    7  cd ~
    8  ls -Fla
    9  which vi
   10  which vim
   11  vi .bashrc
   12  vi .profile
   13  bash ./.profile
   14  ll
   15  alias ll="ls -Fla"
   16  ll
   17  alias vim=vi
   18  vim
   19  pwd
   20  php
   21  ifconfig
   22  sudo apt-get install apache2 -y
   23  ll
   24  ls -Fla
   25  vi .profile
   26  cp ./.profile ./.bash_profile
   27  ll
   28  vi ./.bash_profile 
   29  exit
   30  ll
   31  cat ./.bash_profile 
   32  vi ./.profile
   33  cat /usr/share/doc/bash/examples/startup-files
   34  ls -Fla
   35  vi ./.bashrc
   36  mv ./.bash_profile ./.bash_aliases
   37  exit
   38  ll
   39  sudo apt-get install git
   40  cd Public/
   41  ll
   42  cd ..
   43  ll
   44  mkdir Development
   45  cd Development/
   46  ll
   47  mkdir rgt_3.14
   48  cd rgt_3.14/
   49  ll
   50  echo "# rgt_3.14" >> README.md
   51  git init
   52  git add README.md
   53  git commit -m "first commit"
   54  git remote add origin https://github.com/OnaiNet/rgt_3.14.git
   55  git config --global user.name "Kevin Gwynn"
   56  git config --global user.email "kevin.gwynn@gmail.com"
   57  git push -u origin master
   58  git config --global credential.helper cache
   59  git config --global credential.helper 'cache --timeout=3600'
   60  git push -u origin master
   61  git remote add origin https://github.com/OnaiNet/rgt_3.14.git
   62  git remotes
   63  git remote
   64  git remote -v
   65  git push -u origin master
   66  git commit -m "Initial commit"
   67  git push -u origin master
   68  history
   69  top
   70  ll
   71  history >> commands.md
   72  ll
   73  git add commands.md 
   74  git commit -m "Adding commands.md.."
   75  vi commands.md 
   76  ll
   77  jobs
   78  exit
   79  apt-get help
   80  apt-get update
   81  sudo su -
   82  cd /var/www
   83  ll
   84  cd html
   85  ll
   86  cd ~
   87  ll
   88  cd Development/
   89  ll
   90  cd rgt_3.14/
   91  ll
   92  git pull
   93  git status
   94  git log
   95  ll
   96  cd kgwynn
   97  ll
   98  cd webroot
   99  ll
  100  cd ..
  101  ll
  102  cd ..
  103  ll
  104  mv commands.md kgwynn
  105  ll
  106  cd kgwynn
  107  ll
  108  cd /var/www
  109  ll
  110  cd html
  111  ll
  112  cat index.html 
  113  apachectl
  114  apachectl stop
  115  cd ..
  116  ll
  117  cp html/index.html ~/Development/rgt_3.14/kgwynn/webroot/apache.html
  118  ll
  119  rm -Rf html
  120  sudo rm -Rf html
  121  ln
  122  ln --help
  123  sudo ln -s ~/Development/rgt_3.14/kgwynn/webroot/ html
  124  ll
  125  apachectl start
  126  sudo apachectl start
  127  cd ~
  128  cd Development/
  129  cd rgt_3.14/
  130  cd kgwynn
  131  ll
  132  cd web
  133  cd webroot/
  134  ll
  135  vi index.html 
  136  vi ~/.vimrc
  137  vi index.html 
  138  vi ~/.vimrc
  139  vi index.html 
  140  cd ..
  141  git status
  142  git add commands.md 
  143  git add webroot/index.html 
  144  history
  145  type commands.md
  146  ll
  147  cat commands.md 
  148  history > commands.md 
  149  git status
  150  git add ../commands.md 
  151  git status
  152  git add commands.md
  153  git status
  154  git commit -m "Moved commands.md; Work on index.html"
  155  git push
  156  git status
  157  ll
  158  cd ..
  159  ll
  160  php
  161  php -v
  162  cd kgwynn/webroot/
  163  ll
  164  echo "<?php phpinfo(); ?>" > phpinfo.php
  165  ll
  166  cd ..
  167  ll
  168  vi notes
  169  ll
  170  git status
  171  ll
  172  exit
  173  cd Development/
  174  ll
  175  cd rgt_3.14/
  176  ll
  177  cd kgwynn
  178  ll
  179  vi notes
  180  ll
  181  rm notes
  182  ll
  183  cd ..
  184  git status
  185  git pull
  186  rm kgwynn/webroot/apache.html 
  187  git status
  188  git pull
  189  exit
  190  cd Development/
  191  cd rgt_3.14/
  192  ll
  193  git pull
  194  ifconfig
  195  php
  196  php -v
  197  echo "<?php echo $_SERVER['REMOTE_ADDR']; ?>" | php
  198  echo "<?php echo $SERVER['REMOTE_ADDR']; ?>" | php
  199  echo "<?php print_r($_SERVER); ?>" | php
  200  echo "<?php print_r(\$_SERVER); ?>" | php
  201  echo "<?php echo \$_SERVER['REMOTE_ADDR']; ?>" | php
  202  sudo su -
  203  php -i | less
  204  echo "<?php echo gethosthame(); ?>" | php
  205  echo "<?php echo getHostByName(); ?>" | php
  206  echo "<?php echo getHostByName(getHostName())); ?>" | php
  207  echo "<?php echo getHostByName(getHostName()); ?>" | php
  208  php -r "echo gethostbyname(php_uname('n'));"
  209  php -v
  210  cd kgwynn
  211  ll
  212  ifconfig
  213  ifconfig wlan0
  214  ifconfig -a wlan0
  215  which ifconfig
  216  /sbin/ifconfig eth0 | grep "inet addr" | awk -F: '{print $2}' | awk '{print $1}'
  217  /sbin/ifconfig wlan0 | grep "inet addr" | awk -F: '{print $2}' | awk '{print $1}'
  218  cd ..
  219  cd tools
  220  ll
  221  vi getip
  222  chmod +755 getip
  223  ll
  224  ./getip
  225  jobs
  226  vi getip 
  227  ./getip
  228  cd ..
  229  ll
  230  cd kgwynn
  231  cat ip
  232  ../tools/getip > ip
  233  ll
  234  cat ip
  235  ifconfig
  236  ll
  237  l
  238  cd ..
  239  ll
  240  git status
  241  git add kgwynn/ip
  242  rm kgwynn/webroot/phpinfo
  243  rm kgwynn/webroot/phpinfo.php 
  244  git add tools/getip
  245  git status
  246  git commit -m "Updated IP for kgwynn(home), added 'getip' tool"
  247  cd ..
  248  cd rgt_3.14/
  249  cd tools
  250  ll
  251  vi README.md 
  252  fg
  253  getip
  254  ./getip
  255  fg
  256  cd ..
  257  cd kgwynn
  258  ../tools/getip > ip
  259  git status
  260  fg
  261  cd ..
  262  git status
  263  git add tools/README.md 
  264  git commit -m "Updated README.md for tools to show 'getip' usage"
  265  git push
  266  git status
  267  ll
  268  py
  269  python
  270  node
  271  node --version
  272  node -v
  273  cd kgwynn/
  274  ll
  275  cd web
  276  cd webroot/
  277  ll
  278  vi index.html stylesheets/main.css js/main.js 
  279  jobs
  280  cd ..
  281  cd webroot/
  282  ll
  283  ll ..
  284  vi ../notes
  285  ll
  286  cd ..
  287  ll
  288  mkdir php_modules
  289  git ignore
  290  git --help ignore
  291  git status
  292  cd ..
  293  vi ./.gitignore
  294  git status
  295  jobs
  296  cd kgwynn/
  297  ll
  298  cd php_modules/
  299  echo "test" > test
  300  cd ..
  301  git status
  302  vi ./.gitignore
  303  git status
  304  vi ./.gitignore
  305  git status
  306  git add ./.gitignore
  307  cd kgwynn/
  308  ll
  309  cd php_modules
  310  ll
  311  rm test
  312  git clone https://github.com/jublonet/codebird-php.git
  313  ll
  314  cd codebird-php/
  315  ll
  316  cd src
  317  ll
  318  cd ..
  319  c
  320  cd webroot/
  321  ll
  322  cd ..
  323  ll
  324  vi package.json
  325  bower
  326  apt-get install bower
  327  sudo apt-get install bower
  328  npm install -g bower
  329  npm
  330  which npm
  331  which node
  332  sudo apt-get install npm
  333  sudo npm install -g bower
  334  ll
  335  bower init
  336  ll
  337  cd php_modules/
  338  ll
  339  cd ..
  340  ll
  341  history | less
  342  bower install codebird-php --save
  343  ll
  344  cd bower_components/
  345  ll
  346  cd codebird-php/
  347  ll
  348  cd src
  349  ll
  350  cd ..
  351  ll
  352  cd ..
  353  ll
  354  rm -Rf php_modules/
  355  cd ..
  356  git status
  357  vi ./.gitignore
  358  git status
  359  jobs
  360  cd kgwynn/
  361  ll
  362  cd webroot/
  363  ll
  364  cd ../../
  365  git status
  366  fg
  367  jobs
  368  git status
  369  git add kgwynn/bower.json 
  370  cd kgwynn
  371  ll
  372  rm package.json 
  373  cd ..
  374  ll
  375  cd kgwynn/
  376  ll
  377  cd webroot/
  378  ll
  379  cd ..
  380  ll
  381  git status
  382  git add ./.gitignore
  383  git add kgwynn/webroot/stylesheets/main.css 
  384  git status
  385  git commit -m "Set up bower to download codebird-php; tweaks to main.css"
  386  git push
  387  git status
  388  ll
  389  git pull
  390  cd kgwynn/
  391  ll
  392  cd webroot/
  393  ll
  394  cd tweet
  395  ll
  396  vi index.php 
  397  php index.php 
  398  ll ../../bower_components/codebird-php/
  399  ll ../../bower_components/codebird-php/src
  400  jobs
  401  vi index.php 
  402  php index.php 
  403  fg
  404  php index.php 
  405  fg
  406  vi index.php 
  407  cd ..
  408  ll
  409  vi index.html 
  410  vi js/main.js 
  411  jobs
  412  cd ..
  413  ll
  414  cd ..
  415  ll
  416  git status
  417  git add kgwynn/webroot/js/main.js 
  418  git add kgwynn/webroot/tweet/index.php 
  419  git commit -m "Now tweeting my output to #rgt_output!"
  420  git push
  421  ll
  422  ssh kgwynn@lizandkevin.info
  423  git pull
  424  top
  425  kill 2874
  426  top
  427  exit
  428  ifconfig
  429  cd r
  430  ll
  431  cd pi
  432  ll
  433  cd De
  434  cd Development/
  435  ll
  436  cd rgt_3.14/
  437  ll
  438  cd tools
  439  ll
  440  cd ..
  441  cd kgwynn
  442  ../tools/ip
  443  ../tools/getip
  444  ../tools/getip > ip
  445  git status
  446  git diff ip
  447  git add ip && git commit -m 'Updated IP address for CHG Guest'
  448  git push
  449  cd ..
  450  git status
  451  git push
  452  ifconfig
  453  pwd
  454  git pull
  455  ll
  456  cd tools
  457  ll
  458  ./.getip
  459  ./getip
  460  chmod 755 sethosts 
  461  git status
  462  git commit -m "Set execute flags on sethosts" && git push
  463  git add sethosts && git commit -m "Set execute flags on sethosts" && git push
  464  sudo raspbi-config
  465  sudo raspi-config
  466  ll
  467  cd ..
  468  cd kgwynn/
  469  ll
  470  history
  471  ll
  472  history > commands.md 
  473  exit
  474  sudo raspi-config
  475  ifconfig
  476  ll
  477  cd De
  478  cd Development/rgt_3.14/
  479  ll
  480  cd tool
  481  ll
  482  cd tools
  483  ll
  484  vi getip
  485  ifconfig wlan0
  486  ifconfig eth0
  487  jobs
  488  vi getip
  489  ./getip
  490  fg
  491  ./getip
  492  fg
  493  ./getip
  494  fg
  495  ./getip
  496  fg
  497  ./getip
  498  fg
  499  ./getip
  500  fg
  501  ./getip
  502  ifconfig
  503  jobs
  504  fg
  505  ./getip
  506  fg
  507  ./getip
  508  fg
  509  ./getip
  510  fg
  511  ./getip
  512  ./getip && echo Hi
  513  ./getip || echo Hi
  514  fg
  515  ./getip 2>/dev/null
  516  ./getip 1>/dev/null
  517  fg
  518  ./getip
  519  git status
  520  git add getip
  521  git commit -m "Updated getip to show either ethernet or wifi IP" && git push
  522  sudo raspi-config
  523  which vi
  524  which vim
  525  vim
  526  cd Development/rgt_3.14/
  527  tools/getip
  528  sudo iwlist wlan0 scan
  529  sudo vi /etc/wpa_supplicant/wpa_supplicant.conf 
  530  ll
  531  tools/getip > kgwynn/ip
  532  git status
  533  history > kgwynn/commands.md 
