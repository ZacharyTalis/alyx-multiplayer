# alyx-multiplayer

![alyx-multiplayer logo, a health heart icon of interspersed red and yellow circles.](assets/logo_small.png)

### A multiplayer mod for [Half-Life: Alyx](https://www.half-life.com/en/alyx/) running on Windows.

A lotta folks made this possible, notably:

- DerkO, Lyfeless, and 2838 for writing the original LiveSplit ASL script.
- 2838 and Kube for refactoring this ASL script into a standalone program.
- Kube's partner for some slick debugging.
- All the [LiveSplit](https://github.com/LiveSplit/LiveSplit) repo contributors (most of this code comes from that repo's memory-watching tools).
- The [SuperSimpleTCP](https://github.com/jchristn/simpletcp) contributors and their excellent example code (which we also cribbed).
- The TF2Maps, SourceRuns, and Speedrun Tool Development communities.

This repo's license is a copy of the [LiveSplit license](https://github.com/LiveSplit/LiveSplit/blob/master/LICENSE), since we stole a bunch of their code :)

## Install instructions

[Check the project website](https://alyx-multiplayer.com/#install) for an up-to-date installation guide.

## Common compiling issues

- `Only part of a ReadProcessMemory or WriteProcessMemory request was completed`: Make sure you're compiling to x64!