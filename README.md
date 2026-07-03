Sky Roller 3D — Endless Survival

An endless survival game built in Unity. You control a rolling ball that moves forward automatically across a procedurally generated track. Steer left and right to stay on the platforms, dodge hazards, and survive as long as possible — the further you travel, the higher your score.
Setup & Run

    Download the ZIP and extract it. Replace the main project folder with the internal folder and its contents.
    In Unity Hub, choose Add → Add project from disk and select the project folder. Open it with Unity version 6000.4.6f1.
    In the Project window, open the Scenes folder and open the MainMenu scene.
    Once the scene finishes loading, press Play, then click Play on the main menu to start a run.

Controls

    Left / Right — A and D, or the arrow keys (steer the ball side to side)
    Forward movement is automatic — the ball always rolls ahead on its own.

How to Play

Stay on the track and avoid falling off. The path occasionally shifts one lane to the left or right, so keep steering to follow it. Watch for hazard patches and obstacles. Your score is the distance you travel, shown at the top of the screen. Falling off ends the run, and you can restart or return to the menu from the Game Over screen.
Features
Procedural platform generation

The track is built at runtime by a generator that spawns new platform sections ahead of the player as they move forward and removes old sections behind them, so the scene never fills up. The path snaps to discrete lanes (one block-width apart) and occasionally shifts sideways for variety, while keeping every section reachable.
Four platform prefabs

The generator draws from four distinct platform types to create variety:

    Plain — safe ground, and the default most sections use.
    Mud (Slow) — a brown patch that temporarily slows the ball down.
    Ice (Slippery) — a blue patch that makes steering sluggish and hard to control.
    Saw — a platform carrying a moving obstacle that ends the run on contact.

The opening stretch is always plain to give the player a fair start, and hazards appear at a controlled frequency so the game stays playable.
Three hazard types (beyond falling)

    Slow zones reduce the ball's speed for a short time.
    Slippery zones reduce steering control, making the ball harder to maneuver.
    Moving saw obstacles end the run immediately on contact.

There is also a speed boost pickup that temporarily increases forward speed as a positive counterpart to the hazards.
Survival score

The score tracks the distance traveled and is displayed on screen with UI text during play. The final distance is also shown on the Game Over screen.
Lose condition & restart flow

Falling off the platforms ends the run: the ball stops and a Game Over screen appears showing the final distance. From there the player can Restart to begin a fresh run or return to the Main Menu.
Camera follow

A smoothed follow camera tracks the ball as it rolls forward and drifts between lanes.
Built With

    Unity 6000.4.6f1
    C# (Unity Input System, TextMeshPro)
