# Game a Week Template

This repository is provided for you to create, work on, and submit your project source for each theme of the Game a Week assessment at Manchester Metropolitan University. You will be given a separate repository for each theme.

The provided .gitignore covers a wide range of game engines. Please make sure that the projects you create are placed in the right place of the repository (or edit/relocate the gitignore file).

A reminder to never ever clone a repository to a folder that is managed by OneDrive (which includes your Desktop).


# How to use this week's template

This week's repository comes with an existing project that you will need to work with to make your own microgame for the Microgame Mash Up!
Games should be created in the form of a prefab that can be loaded automatically.

To get started, clone the repository to an empty folder, then open the project in Unity version 6000.0.51f (the default for university machines).

You will be prompted to enter your student ID. Enter this and the project will generate a folder, game prefab and GameManager script for you.
To find these, navigate to Assets/Resources/"your student number" (e.g. Assets/Resources/12345678). Inside this folder you will find a GameManager.cs and a prefab with your student number in the name.

Your game prefab should open when it is created, but you can open this as with any prefab to edit your game at any time.
The prefab root will have both a GameInfo and GameManager script. Add your game's information in the GameInfo script's fields, but do not edit the GameInfo class itself.

Instead, your game's logic should live in the generated GameManager class, which contains starter code with blank game event functions that you can fill in with your own custom code.

The GameMaster class contains a series of static access functions so that you can control your game, which you may wish to access from your GameManager class.

Static functions available are:

- GameMaster.GetTotalTime() // The total time for the current game in seconds.
- GameMaster.GetTimeElapsed() // The time elapsed for the current game in seconds.
- GameMaster.GetFractionTimeElapsed() // The time elapsed for the current game as a 0-1 fraction.
- GameMaster.GetTimeRemaining() // The time remaining for the current game in seconds.
- GameMaster.GetFractionTimeRemaining() //The time remaining for the current game as a 0-1 fraction.
- GameMaster.GetGameState() // The game's current state as a Game.State. This can be either NOT_STARTED, RUNNING, SUCCESS or FAILED.
- GameMaster.GetDifficulty() // The requested difficulty level for the current game as a GameMaster.Difficulty. This can be either VERY_EASY, EASY, NORMAL, HARD or VERY HARD.

- GameMaster.GameSucceeded() // A method that should be called when your game is completed successfully.

Games in the Microgame Mash Up are played in a randomised order, with the difficulty and speed increasing with each game, until the player runs out of lives.
Difficulty ranges from "Very Easy" to "Very Hard", with this increasing every five games.
During each difficulty level, games will increase in speed, starting with 20 seconds on the timer, decreasing to 10 seconds before the next level up in difficulty.
Games should therefore aim to make use of the difficulty level, as well as the increased speed.

When starting the game from the main menu (which will autoload when hitting the play button), the difficulty will start at Very Easy. To test higher difficulty levels without waiting or playing through your game multiple times, you can make use of the menu options available, which can be found in the top bar of the Unity editor under "Party Pack/Set Difficulty".


# Multiple Games

Can I make multiple games? - Whilst we'd generally advise that one great game is better than many smaller ones, if you're wanting to be playful with a mechanic across multiple games, this can be achieved.
You will first need to duplicate your game prefab and game manager script, renaming these appropriately. In your new game prefab, you should then remove the GameManager script from the prefab root, and add your new game manager script in it's place.


# Video Help

A playlist of videos going through how to use the game pack to create a simple game, how to use the GameMaster functions, and how to create a second game can be found here: https://mmutube.mmu.ac.uk/playlist/details/1_e4sqosx7

