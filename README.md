# alyx-multiplayer

![alyx-multiplayer logo, a health heart icon of interspersed red and yellow circles.](assets/logo_small.png)

### A multiplayer mod for Alyx. More info coming to this readme soon!

A lotta folks made this possible, notably:

- DerkO, Lyfeless, and 2838 for writing the original [LiveSplit](https://github.com/LiveSplit/LiveSplit) ASL script.
- 2838 and Kube for refactoring this ASL script into a standalone program. Kube's partner for some slick debugging.
- All the LiveSplit repo contributors (most of this code comes from that repo's memory-watching tools).
- The TF2Maps, SourceRuns, and Speedrun Tool Development communities.

This repo's license is a copy of the [LiveSplit license](https://github.com/LiveSplit/LiveSplit/blob/master/LICENSE), since we stole a bunch of their code :)

## Common compiling issues

- `Only part of a ReadProcessMemory or WriteProcessMemory request was completed`: Make sure you're compiling to x64!