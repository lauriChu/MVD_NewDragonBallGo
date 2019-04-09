# MVD_NewDragonBallGo
NewDragonBallGo mobile game made with Unity

## Description
DragonBallGo consists of a online videogame for mobile platforms developed with the Unity engine. This videogame is an exercise for MVD, where we have to practise and learn Unity programming.

The video game consists of looking for the seven dragon balls that characterize the anime on which it is inspired. For this, the player must use the 3d - radar map to find them.
Once the user finds a ball, he must make a game of scissors paper stone, choosing a combination. Depending on the result you will get the ball or not.

## Technical Requirements
- MapBox for render 3dMap.
- InputLocation for manage user current location.
- UnityWebRequest for comunicates with external Rest API.
- Mock API - In development - did it with Postman.
- Animations.
- Mp4 and Gif players.
- Augmented Reality with Vuforia.
- iOS Build.

## Impediments
- Vuforia does not allow the visualization of 3d objects without being related to a recognized target. Therefore, the visualization of reality is investigated manually using the camera, oscilloscope and pedometer. The result is not as expected and it is decided to eliminate directly from the game.

- It was not possible to update the mapbox version since it implies redoing all the programming of the part of the 3d map display and part of the user's location. Due to lack of time, the possibility of development is ruled out.

- A considerable amount of time has been spent on remaking the entire UI, both at an artistic level and in resources as well as in animations.

- It has been spent more time than expected to update to update the project from version 5.6f to the current Unity.

- It has had to be part of an API mock since the server is no longer operational.

- Considerable time has been spent adding loading screens and transitions between them.

- Spent more time than expected in Audio management and resource search.

## Improvements
- Add a good quality Aumented reality functionality for search the ball.
- Finish API for work with a real online game
- Updated Mabox SDK
- Add new minigames for get balls
- Add user customization, as well as a section where you can buy themes and / or characters for the game.
- Add users ranking.
- Add chat for groups or games.
- Be able to invite other members more easily.

## Screenshots

Left to Right - Reflection - Pearl Brightness Reflection - Iridescence

![alt front](https://github.com/lauriChu/MVD_FirstCustomShader/blob/master/MVD_15_Shadows-master/1.PNG)

Left to RIght - Iridescence - Pearl Brightness Reflection - Reflection

![alt back](https://github.com/lauriChu/MVD_FirstCustomShader/blob/master/MVD_15_Shadows-master/2.PNG)

## Links
[https://www.gamedev.net/blogs/entry/2264817-idea-iridescent-shader/](https://www.gamedev.net/blogs/entry/2264817-idea-iridescent-shader/)


