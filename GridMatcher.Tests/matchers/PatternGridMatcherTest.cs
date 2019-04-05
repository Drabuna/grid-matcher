using System;
using System.Collections.Generic;
using GridMatcher.Core;
using GridMatcher.Generators;
using GridMatcher.Loaders;
using GridMatcher.Matchers.Pattern;
using GridMatcher.Types;
using NUnit.Framework;

namespace GridMatcher.Tests.matchers
{
    [TestFixture]
    public class PatternGridMatcherTest
    {
        private List<Match> SetupAndFindMatches(Size gridSize, string tileMap, string patternFile)
        {
            var grid = new Grid(gridSize);

            var generator = new InputGridGenerator();
            generator.Input = tileMap;
            generator.GenerateGrid(grid);
            
            var patterns = new PatternsLoader().Load(patternFile);

            var matcher = new PatternGridMatcher();
            matcher.Patterns = patterns;

            return matcher.FindMatches(grid);
        }
        
        [TestCase]
        public void When_GivenMatch3Patterns_Expect_FindMatches()
        {
            //should contain 1 green horizontal m3, 1 yellow vertical m3
            var map = @"[[0,0,0,0,0,0,0],[0,2,0,0,0,0,0],[0,2,0,0,0,0,0],[0,2,0,0,0,0,0],[0,0,0,3,3,3,0],[0,0,0,0,0,0,0]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match3_patterns.json");
            
            Assert.That(matches[0].Name == @"Vertical Match 3");
            Assert.That(matches[0].Tiles.Length == 3);
            Assert.That(matches[0].Tiles[0].X == 1 && matches[0].Tiles[0].Y == 1  && matches[0].Tiles[0].Type == TileType.Green);
            Assert.That(matches[0].Tiles[1].X == 1 && matches[0].Tiles[1].Y == 2  && matches[0].Tiles[1].Type == TileType.Green);
            Assert.That(matches[0].Tiles[2].X == 1 && matches[0].Tiles[2].Y == 3  && matches[0].Tiles[2].Type == TileType.Green);
            
            Assert.That(matches[1].Name == @"Horizontal Match 3");
            Assert.That(matches[1].Tiles.Length == 3);
            Assert.That(matches[1].Tiles[0].X == 3 && matches[1].Tiles[0].Y == 4  && matches[1].Tiles[0].Type == TileType.Yellow);
            Assert.That(matches[1].Tiles[1].X == 4 && matches[1].Tiles[1].Y == 4  && matches[1].Tiles[1].Type == TileType.Yellow);
            Assert.That(matches[1].Tiles[2].X == 5 && matches[1].Tiles[2].Y == 4  && matches[1].Tiles[2].Type == TileType.Yellow);
            
            Assert.That(matches.Count == 2);
        }

        [TestCase]
        public void When_GivenMatch4Patterns_Expect_FindMatches()
        {
            //should contain 1 green horizontal m4, 1 red vertical m4
            var map = @"[[0,0,0,0,0,0,0],[0,1,0,0,0,0,0],[0,1,2,2,2,2,0],[0,1,0,0,0,0,0],[0,1,0,0,0,0,0],[0,0,0,0,0,0,0]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match4_patterns.json");
            
            Assert.That(matches[0].Name == @"Vertical Match 4");
            Assert.That(matches[0].Tiles.Length == 4);
            Assert.That(matches[0].Tiles[0].X == 1 && matches[0].Tiles[0].Y == 1  && matches[0].Tiles[0].Type == TileType.Red);
            Assert.That(matches[0].Tiles[1].X == 1 && matches[0].Tiles[1].Y == 2  && matches[0].Tiles[1].Type == TileType.Red);
            Assert.That(matches[0].Tiles[2].X == 1 && matches[0].Tiles[2].Y == 3  && matches[0].Tiles[2].Type == TileType.Red);
            Assert.That(matches[0].Tiles[3].X == 1 && matches[0].Tiles[3].Y == 4  && matches[0].Tiles[3].Type == TileType.Red);

            
            Assert.That(matches[1].Name == @"Horizontal Match 4");
            Assert.That(matches[1].Tiles.Length == 4);
            Assert.That(matches[1].Tiles[0].X == 2 && matches[1].Tiles[0].Y == 2  && matches[1].Tiles[0].Type == TileType.Green);
            Assert.That(matches[1].Tiles[1].X == 3 && matches[1].Tiles[1].Y == 2  && matches[1].Tiles[1].Type == TileType.Green);
            Assert.That(matches[1].Tiles[2].X == 4 && matches[1].Tiles[2].Y == 2  && matches[1].Tiles[2].Type == TileType.Green);
            Assert.That(matches[1].Tiles[3].X == 5 && matches[1].Tiles[3].Y == 2  && matches[1].Tiles[3].Type == TileType.Green);

            Assert.That(matches.Count == 2);
        }

        [TestCase]
        public void When_GivenMatch3And4Patterns_Expect_FindMatches()
        {
            //Each Match 4, effectively contains 2 possible match permutations
            /*
             So this map should contain:
              Vertical Match 3 [0, 1, Yellow], [0, 2, Yellow], [0, 3, Yellow]
              
              Vertical Match 4 [1, 1, Red], [1, 2, Red], [1, 3, Red], [1, 4, Red]
              Vertical Match 3 [1, 1, Red], [1, 2, Red], [1, 3, Red] (permutation 1)
              Vertical Match 3 [1, 2, Red], [1, 3, Red], [1, 4, Red] (permutation 2)

              Horizontal Match 4 [2, 2, Green], [3, 2, Green], [4, 2, Green], [5, 2, Green]
              Horizontal Match 3 [2, 2, Green], [3, 2, Green], [4, 2, Green] (perm 1)
              Horizontal Match 3 [3, 2, Green], [4, 2, Green], [5, 2, Green] (perm 2)
              
              Horizontal Match 3 [3, 5, Green], [4, 5, Green], [5, 5, Green]
            */
            
            var map = @"[[2,3,2,3,2,3,2],[3,1,3,2,3,2,3],[3,1,2,2,2,2,1],[3,1,1,3,1,3,1],[1,1,3,2,2,1,3],[2,2,1,2,2,2,3]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match3and4_patterns.json");

            Assert.That(matches.FindAll(match => match.Name == "Vertical Match 3" && match.Tiles.Length == 3 && match.TilesColorsMatch(TileType.Red)).Count == 2);      
            Assert.That(matches.FindAll(match => match.Name == "Horizontal Match 3" && match.Tiles.Length == 3 && match.TilesColorsMatch(TileType.Green)).Count == 3);          
            Assert.That(matches.FindAll(match => match.Name == "Vertical Match 3" && match.Tiles.Length == 3 && match.TilesColorsMatch(TileType.Yellow)).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Vertical Match 4" && match.Tiles.Length == 4 && match.TilesColorsMatch(TileType.Red)).Count == 1);   
            Assert.That(matches.FindAll(match => match.Name == "Horizontal Match 4" && match.Tiles.Length == 4 && match.TilesColorsMatch(TileType.Green)).Count == 1);      

            Assert.That(matches.Count == 8);

        }

        [TestCase]
        public void When_GivenMatch4SquarePatterns_Expect_FindMatches()
        {
            var map = @"[[2,2,0,2,1,1,2],[2,2,0,2,2,2,2],[0,0,0,0,3,3,0],[0,0,0,0,0,0,0],[1,2,3,0,2,2,0],[2,2,0,0,1,2,3]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match4square_patterns.json");
           
            /**
             *  Match - Square Match 4 [0, 0, Green], [1, 0, Green], [0, 1, Green], [1, 1, Green]
                Match - Square Match 4 With Red And Yellow Variation 3 [0, 4, Red], [1, 4, Green], [2, 4, Yellow], [0, 5, Green], [1, 5, Green]
                Match - Square Match 4 With Red And Yellow Variation 1 [3, 0, Green], [4, 0, Red], [3, 1, Green], [4, 1, Green], [4, 2, Yellow]
                Match - Square Match 4 With Red And Yellow Variation 4 [4, 4, Green], [5, 4, Green], [4, 5, Red], [5, 5, Green], [6, 5, Yellow]
                Match - Square Match 4 With Red And Yellow Variation 2 [5, 0, Red], [6, 0, Green], [5, 1, Green], [6, 1, Green], [5, 2, Yellow]
             */
           
            Assert.That(matches.FindAll(match => match.Name == "Square Match 4" && match.Tiles.Length == 4 && match.TilesColorsMatch(TileType.Green)).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Square Match 4 With Red And Yellow Variation 1" && match.Tiles.Length == 5).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Square Match 4 With Red And Yellow Variation 2" && match.Tiles.Length == 5).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Square Match 4 With Red And Yellow Variation 3" && match.Tiles.Length == 5).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Square Match 4 With Red And Yellow Variation 4" && match.Tiles.Length == 5).Count == 1);      
            
            Assert.That(matches.Count == 5);
        }

        [TestCase]
        public void When_GivenMatch5Patterns_Expect_FindMatches()
        {
            var map = @"[[1,0,0,0,0,0,0],[1,0,0,0,0,0,0],[1,0,0,0,0,0,0],[1,2,2,2,2,2,0],[1,0,0,0,0,0,0],[0,0,0,0,0,0,0]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match5_patterns.json");

            Assert.That(matches.FindAll(match => match.Name == "Vertical Match 5" && match.Tiles.Length == 5 && match.TilesColorsMatch(TileType.Red)).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Horizontal Match 5" && match.Tiles.Length == 5 && match.TilesColorsMatch(TileType.Green)).Count == 1);      

            Assert.That(matches.Count == 2);
        }

        [TestCase]
        public void When_GivenMatch5TandLPatterns_Expect_FindMatches()
        {
            var map = @"[[2,0,0,0,2,1,2],[1,2,2,0,0,2,0],[2,0,0,0,0,2,0],[0,2,0,2,2,1,0],[0,2,0,0,0,2,0],[0,1,2,2,0,2,0]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match5TandL_patterns.json");

            Assert.That(matches.FindAll(match => match.Name == "Match 5 T1" && match.Tiles.Length == 5 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1
                        ).Count == 1);      
            Assert.That(matches.FindAll(match => match.Name == "Match 5 T2" && match.Tiles.Length == 5 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1
                        ).Count == 1);    
            Assert.That(matches.FindAll(match => match.Name == "Match 5 L1" && match.Tiles.Length == 5 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1
                        ).Count == 1);    
            Assert.That(matches.FindAll(match => match.Name == "Match 5 L2" && match.Tiles.Length == 5 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1
                        ).Count == 1);    
            Assert.That(matches.Count == 4);
        }

        [TestCase]
        public void When_GivenMatch5andExtraPatterns_Expect_FindMatches()
        {
            var map = @"[[0,0,0,0,0,0,0],[3,2,1,2,3,0,0],[0,0,2,0,0,0,0],[0,0,2,0,0,0,0],[0,0,0,0,0,0,0],[0,0,0,0,0,0,0]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match5andExtra_patterns.json");
            
            Assert.That(matches.FindAll(match => match.Name == "Match 5 Extra Chips" && match.Tiles.Length == 7 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Yellow).Length == 2
                        ).Count == 1);
            
            Assert.That(matches.Count == 1);
        }
        
        [TestCase]
        public void When_GivenMatch5LandExtraPatterns_Expect_FindMatches()
        {
            var map = @"[[0,0,0,0,0,0,0],[3,1,2,2,0,0,0],[0,2,0,0,0,0,2],[0,2,0,0,0,0,2],[0,0,0,0,2,2,1],[0,0,0,0,0,0,3]]";
            var matches = SetupAndFindMatches(new Size(7, 6), map, @"resources\match5LandExtra_patterns.json");
            
            Assert.That(matches.FindAll(match => match.Name == "Match 5 L1 With Extra Chips" && match.Tiles.Length == 6 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Yellow).Length == 1
                        ).Count == 1);
            
            Assert.That(matches.FindAll(match => match.Name == "Match 5 L2 With Extra Chips" && match.Tiles.Length == 6 && 
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Green).Length == 4 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Red).Length == 1 &&
                                                 Array.FindAll(match.Tiles, tile => tile.Type == TileType.Yellow).Length == 1
                        ).Count == 1);
            
            Assert.That(matches.Count == 2);
        }
        
    }
}