
# For running Python scripts:
I used a [virtual environment](https://docs.python.org/3/tutorial/venv.html)

Using powershell in the directory where your python script is, run:
1. `python -m venv <env name here>` to create the environment folder, it's not checked into source control
   * if it's your first time, you'll likey need to [install the modules you want via the PIP](https://www.w3schools.com/python/python_pip.asp), e.g. `pip install <module name>`
2. run `<env name>\Scripts\activate.bat`
3. execute script: `python .\<script name>.py`

