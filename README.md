# UnityReplaySystem
Project demonstrating how to use input logging to create a replay system, where the replay is viewed by replaying the inputs with a deterministic fixed timestep.

![Tanks battling in play mode](http://i.imgur.com/6S1x3tB.jpg)
![Tanks battling in replay mode](http://i.imgur.com/uxzCquN.png)

[Video demonstration here](https://www.youtube.com/watch?v=2P55Ujhnavc)

Game runs on a fixed timestep to ensure that it plays out deterministically regardless of framerate. Inputs are polled during the Update loop and then "flattened" into a single input structure to be queried during the next FixedUpdate.

These inputs are cached by the `ReplayLogger` class, which in turn are played back by the `ReplayInputController` after the game has completed.

VHS effect is from [this repository](https://github.com/staffantan/unity-vhsglitch).
