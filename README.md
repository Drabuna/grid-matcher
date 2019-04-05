## Dependencies
* Net Core 3.0
* Json. NET
* NUnit for unit testing

## How to build and run tests
Clone the repo and then:

    cd .\grid-matcher\
    dotnet restore
    dotnet build

To run tests:

    dotnet test

## Project
### Core
**Grid** is a container for all the tiles, holds them in form of two dimensional array. Exposes an API to get and set **tiles**.
**Tile** is the basic building block, just holds the X and Y grid index, and a **TileType**.
**TileType** enum of all possible tile types, we are working with 4 - empty, red, green, yellow.

### Generators
Simply used to populate grid in some specific way.
**RandomGridGenerator** - randomly places non-empty Tiles on the grid, seed can be provided.
**InputGridGenerator** - given a 2 dimensional JSON array as a string input, will place tiles in a specified order. 

### Matchers
Given a **Grid** will try to return matches based on specific implementation.
For now I'm going with pattern matching approach, the idea behind it is as follows:
An algorithm will be provided with array of possible matching patterns. A pattern is a list of X and Y index offsets and color specifiers. So a pattern of [{x:0, y:0}, {x: 1, y:0}, {x:2, y:0}] will describe a simple horizontal match-3 pattern. What we additional need to be able to give an extendable solution is color specifiers. In this version following is supported:
* Any - matches any tile except for empty ones
* LikeFirst - matches the tile color of the first tile in the sequence. So let's say if the first one is green, LikeFirst will reference green color.
* Red, Green, Yellow - will match those specific colors.

So, **Pattern** is used for describing the patterns and **PatternGridMatcher** expect a **Grid** and array of **Pattern** and will return a list of **Matches** if any are found.

### Loaders
Should contain classes that will load and parse the information required. Currently it's just **PatternsLoader** that expects a path to a JSON file. Will try to map it to array of **Pattern**.

### Resources
Contains all the additional resources like JSON files. All the possible patterns are stored there in **patterns.json**

## Unit tests
Basic functionality and all of the required use cases are covered by Unit tests. Matching logic is tested in **PatternGridMatcherTest** class, and it uses specifically configured patterns from test-**resources**.
You can produce new maps via [this dirty editor](https://drabuna.github.io/grid-quick-edit/).

## Future improvements
* If we would to implement blockers, then most likely we should move from Tile representing the content, and make Tiles contain an array of elements. And element could be a Color Chip, a blocker, and whatever else. And in this case one tile will be able to hold multiple elements (so like block covering a chip).
* PatternGridMatcher will produce all the possible matches in different permutations. So if you would to configure to support both match-3 and match-4 patterns, one match-4 pattern will also yield 2 match-3 cases. This could be easily improved by just having some priority order for patterns (so longer patterns would be found first), and then we can actually black-list tiles that were already matched.
* Obviously maps should be loaded from external files
* Will performance be an issue at any point? 
