
I used a virtual environment to run this script
https://docs.python.org/3/tutorial/venv.html

Using powershell in this directory where your python script is, run:
1. The web-scraper-env isn't checked into source control, I think you just run `python -m venv web-scraper-env` to create it?
   1. if it's your first time, you'll likey need to install the modules you want via the PIP
      1. https://www.w3schools.com/python/python_pip.asp (e.g. `pip install requests`)
2. web-scraper-env\Scripts\activate.bat
3. python .\WebScraper.py

