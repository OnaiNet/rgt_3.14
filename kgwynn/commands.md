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
