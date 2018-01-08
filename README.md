# UpdateLib
UpdateLib is a mini-project for downloading and unpacking downloaded packages for simple distribution. 

The project resides in AutoUpdate/Form1.cs path. The project is build as a invisible windows form app for convenience

The necessary paths should be filled at app.config file for updating process.

updatepath: The path of update files.

processpath: The default path for starting project. Eg: @"C:\Program Files (x86)\BlaBlaService\blabla.exe".

processname:The default path of initial process of the program which will be updated(It'll be killed) Eg: "blabla" (just name as appears in task manager).

installpath: Installation path.

temppath: A temp path for temporary files (Files will be deleted after update).

```
<add key="updatepath" value="[UPDATE-SERVER-PATH]" />
<add key="processpath" value="[PROCESS-PATH]" />
<add key="processname" value="[PROCESS-NAME]" />
<add key="installpath" value="[INSTALL-PATH]" />
<add key="temppath" value="[TEMP-PATH]" />
```
Update Server should have 2 files. update.txt which contains version number and update.zip which contains update files.

App compares the update.txt in server and local files (if any exists) and downloads the zip if the local version number is smaller or not exists.
Update server should have the necessary permissions.




