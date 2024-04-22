# Technical task: Simple Wizard Shooter

This technical task was made by myself for the Unity Developer position. I needed to implement a Unity 3D game to show my Unity Engine skills. The original task text is provided below. I added some enhancements to this technical task which are listed in the following section.
The level is a square map with obstacles. The main scene's name is MainScene. Enemy models were assembled from cubes in Unity, it was faster this way.
The description of the settings is made with Unity Tooltips. All RFX1_... scripts are scripts from the special effects package.

## The original task
1) There is a magician on the scene (with health, defense, and movement speed)
2) The magician must be able to move around the scene, turn (using the arrow keys on the keyboard), and be able to cast spells (using **X** on the keyboard)
3) The magician’s spells must be of several types (with different appearance and damage)
4) The magician must have the ability to change the current spell (buttons **Q** and **W** on the keyboard)
5) Monsters must be of several types (with different appearance, amount of health, damage, defense and movement speed)
6) Monsters should be generated randomly beyond the field of view of the magician and sent towards the magician
7) There should be no more than 10 monsters on the stage at a time; when one dies, the next one should be born
8) When a spell hits a monster, its health should decrease according to the damage of the spell and the defense of the monster
9) In case of a collision with a magician, the magician's health should decrease according to the defense of the magician and the damage of the monster

The size of the scene must be limited.
If desired, various obstacles can be placed on the stage.
The appearance of the monsters should be different. You can use simple 3D models or just different colors.
Damage calculation: health = health - damage * defense (0...1).
You should not spend a lot of time on the graphical component - it does not affect the evaluation of the solution. You can use ready-made assets or simple figures.
While performing the test task, do not forget about the extensibility, flexibility, and support costs. Also, keep in mind the performance of the resulting solution. The final result should have the properties of production code, to the extent possible under the conditions of a limited time. ECS cannot be used.
If you couldn't fix all the problems due to lack of time, describe how to get rid of them if it were production code.

## My improvements to the task
I also added the following features
1) Conditions for victory and defeat. To win you need to kill N enemies. Defeat - be killed.
2) UI of the player’s health, the selected spell, and the number of enemies that must be killed to win.
3) Enemy health UI and its name
4) UI of the match start screen, defeat/victory screen
5) Added the possibility to restart the level
6) Enemies spawn either behind the player or at specified points on the level (it checks if the player is not seeing the spawn point)
7) Special effects of spells were imported from a third-party plugin I once purchased. There are also collision effects, sounds, and the arena fence
8) Upon reaching the player, the enemy deals damage and stops for a while, then continues to catch up with the player
9) Added some visuals to make it look prettier

## What I could have improved but did not because of limited time:
1) To optimize the loading of the scene on mobile devices (assuming that it is being developed for them), an almost empty starting scene should be made. The purpose of this empty scene is just to open MainScene.
2) Add animations to enemies
3) Expand the AI of enemies: some run away after an attack, others do something else, etc.
4) Improve the loading of special effects at the beginning of the game. There is currently a slight lag when the first special effect spawns
5) It is possible to include some DI package and use it in the project. I did not bother with it. The singletons were enough for this task IMO.
6) The Name UI block should be extracted into a separate UI component (now it is in the HealthBar). It was one of the last things I added and time was running out.
7) RFX1_TransformMotion from the special effects package was modified directly. Perhaps it should have been inherited or whatever, but it was just faster to modify the script.
8) Remove Raycast Target from UI images and TextMeshPro texts. In this case, it would not have made any difference, but it’s good practice to always remove Raycast Target when it makes sense to do so.
