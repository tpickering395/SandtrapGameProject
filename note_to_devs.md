For the Junior Programmers, if your build doesn't work after pulling the commit that includes the newly included JSON library then please follow these instructions:

### Instructions

* [ ] First make sure your git pull was successful.


* [ ] Make sure a plugins folder exists in your unity directory. If it does make sure a LitJSON.dll file is in there. (A .meta file may exist in there but that's fine). If there is no plugins folder and therefore no LitJSON.dll, make a plugins folder and ask me for the .dll (or compile it yourself in VS2019 as a class library).


* [ ] Add the library as a reference in the ItemDatabase script.

(How? Look in your solution explorer in Visual Studio and click the drop down on References and then click (do not double click) on "Analyzers". At this point go to Project > Add Reference, and add the LitJSON.dll from the plugins folder. If it worked the IntelliSense in Visual Studio should detect it and highlight it. Also, Unity shouldn't throw errors anymore.)

