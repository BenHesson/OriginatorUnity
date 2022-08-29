# OriginatorKids Unity

Running the project: 

The project was tested using Unity 2020.3.38f1
The scene to view the runtime generated sphere is in Assets\Frontend\Scenes\SphereDisplay.unity.
Open the scene and select play.
The sphere should now show in the window.


#### There are two main folders: Frontend and Backend.

In the frontend folder is the FrontEndApi.cs script as well as control script for the sphere called SphereDisplayControl.cs.
- SphereDisplayControl.cs is the main entry point and calls FrontEndApi.cs and the various backend services needed to build the sphere.


In the backend folder there are three main areas: Communication, Parsing, and Tests.

- Communication handles the connection with the server and returns the sphere text file
- Parsing is responsible for the logic to properly create the sphere. 
It's setup in a way that easily allows for other objects to be parsed as well as other versions of the sphere file.
- Tests exist in the editor since they aren't needed to run at runtime. They encompass mostly parsing with some minor checks of the UnityWebRequest object.
Ideally we would have a broader integration test that can ping the server and load the file. As a bonus, it would be nice to have a test that could screenshot the sphere and then do an image compare to make sure nothing has changed.

Item of note:
- The backend could is put inside it's own assembly definition. This way the modifiers like "internal" are respected since it's in its own assembly. Assembly.cs also exists in order for the tests to have access to the "internal" methods in the backend assembly.
