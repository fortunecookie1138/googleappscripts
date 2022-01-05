
I used a virtual environment to run this script
https://docs.python.org/3/tutorial/venv.html

Using powershell in this directory where the scraper script is, run:
1. The web-scraper-env isn't checked into source control, I think you just run `python -m venv web-scraper-env` ?
2. web-scraper-env\Scripts\activate.bat (or actually probably just navigate to the activate script location and run it from there)
3. python .\WebScraper.py