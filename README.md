# README
An Avalonia UI port of [Imaginarium](https://github.com/ianhorswill/ImaginariumCore).

## Usage
1. Open `AvaloniaImaginarium.sln`.
2. Start program with debugging enabled.
3. Open a project (navigate to `/Examples` for sample projects).
4. Start imagining!

## Known Issues
1. When using the 'imagine' command (e.g., `imagine 3 cats`) with debugging, the debugger steps in as if there were breakpoints. Without a debugger, the program crashes.
2. The only features implemented so far are loading ontologies, the `imagine` command, and the `help` command. Missing features include graph visualizations and the more interesting commands that let the user alter the ontology.
