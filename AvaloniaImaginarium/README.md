Here's a general run through of crucial files:

`/Assets` contains crucial ImaginariumCore files as well as `/Examples`. You can find example generators under `/Examples/`.

Under `/Views` you can find Imaginer. These files make up the home page UI and its logic.

Imaginer uses various classes:
- `ReplCommands.cs` contains the commands it will recognize.
- `UserPreferences.cs` for now, stores only the user's selected generator's path (e.g., /Assets/Examples/Cats).
- `ImaginariumResponses.cs` is a collection of responses used by ReplCommands to communicate with Imaginer. Maybe this could just belong in ReplCommands
