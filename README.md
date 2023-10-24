# MemoryMatch
This is a memory match game for funtori test using c# and unity

1. In this game I used DOTween for animations. a custom script named DOTweenActions has been made by me which is a component that can animate everything with loops and eases.
2. A TimeManager is also provided for controlling all kinds of timers with return actions at the end of them.
3. RandomNumberArrayGenerator has also been made for generating arrays of integers with or without repeating the number which is a helper class
4. CrossSceneData is a pure c# class for transfering data between scenes like selected levels
5. LevelScriptableObject is a middleman made for making levels and editing them which gets loaded in the beginning of menu scene if the made scriptableObject is placed in Resources folder
6. Core of the game uses MVP pattern. Model: makes cards and check conditions of winning and losing and picking of cards. View: for view I used SpriteRenderers and Unity UI System. Presenter: which is the middleman between model and view, gets player choices in the view and communicate to model for computation
7. for reducing draw calls I used sprite atlas which reduced them significantly beacause many of the sprites where getting called for the cards
8. other draw call reducing techniques are mostly used when 3d objects are involved like gpu instancing, LOD, Static and Dynamic Batching and combining meshes
9. Singleton pattern is also used when absoutly neccesary like the timeManager which gets created as an object.
10. a better pattern to use when a lot of services are getting global references is service locator which has them all in one place.
11. for communication between model and presenter classes beacause of dependecy I used observer pattern to send events to presenter calss fro mmodel class
