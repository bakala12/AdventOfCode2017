using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day21 : AocDay<List<ImageTransformRule>>
    {
        public Day21(IInputParser<List<ImageTransformRule>> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(List<ImageTransformRule> input)
        {
            Execute(input, 5);
        }

        protected override void Part2(List<ImageTransformRule> input)
        {
            Execute(input, 18);
        }

        private static void Execute(List<ImageTransformRule> transformRules, int iterations)
        {
            var img = new bool[3,3]
            {
                { false, true, false }, { false, false, true }, { true, true, true }
            };
            var result = DoIterations(img, transformRules, iterations);
            Console.WriteLine(CountBits(result));
        }

        private static bool[,] DoIterations(bool[,] img, List<ImageTransformRule> transformRules, int iterations)
        {
            for(int i = 0; i < iterations; i++)
                img = DoIteration(img, transformRules);
            return img;
        }

        private static bool[,] DoIteration(bool[,] img, List<ImageTransformRule> transformRules)
        {
            int size = img.GetLength(0) % 2 + 2;
            int squares = img.GetLength(0) / size;
            var result = new bool[squares * size + squares, squares * size + squares];
            var rules = transformRules.Where(r => r.Size == size).ToList();
            for(int sr = 0; sr < squares; sr++)
                for(int sc = 0; sc < squares; sc++)
                {
                    var matchingRule = FindMatchingRule(img, sr*size, sc*size, size, rules);
                    WriteResult(result, matchingRule.Result, sr*size+sr, sc*size+sc, size+1);
                }
            return result;
        }

        private static ImageTransformRule FindMatchingRule(bool[,] img, int row, int column, int size, List<ImageTransformRule> rulesToCheck)
        {
            return rulesToCheck.First(r => IsRuleMatching(r, img, row, column, size));
        }

        private static bool IsRuleMatching(ImageTransformRule rule, bool[,] img, int row, int column, int size)
        {
            foreach(var (rowSelector, columnSelector) in GetAllPaternRotationsAndFlips(size))
                if(TestMatch(img, row, column, size, rule.Pattern, rowSelector, columnSelector))
                    return true;
            return false;
        }

        private static IEnumerable<(Func<int, int, int>, Func<int, int, int>)> GetAllPaternRotationsAndFlips(int size)
        {
            yield return new ((r,c) => r, (r,c) => c); //no rotation
            yield return new ((r,c) => c, (r,c) => size-1-r); //90 rotation clockwise
            yield return new ((r,c) => size-1-r, (r,c) => size-1-c); //180 rotation clockwise
            yield return new ((r,c) => size-1-c, (r,c) => r); //270 rotation clockwise
            yield return new ((r,c) => size-1-r, (r,c) => c); //vertical flip
            yield return new ((r,c) => r, (r,c) => size-1-c); //horizontal flip
            yield return new ((r,c) => size-1-c, (r,c) => size-1-r); //diagonal flip
            yield return new ((r,c) => c, (r,c) => r); //anti-diagonal flip
            
        }

        private static bool TestMatch(bool[,] img, int row, int column, int size, bool[,] pattern, Func<int, int, int> rowSelector, Func<int, int, int> columnSelector)
        {
            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                    if(img[row+i, column+j] != pattern[rowSelector(i,j),columnSelector(i,j)])
                        return false;
            return true;
        }

        private static void WriteResult(bool[,] result, bool[,] ruleResult, int row, int column, int size)
        {
            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                    result[row+i, column+j] = ruleResult[i,j];
        }

        private static int CountBits(bool[,] tab)
        {
            int c = 0;
            for(int i = 0; i < tab.GetLength(0); i++)
                for(int j = 0; j < tab.GetLength(1); j++)
                    if(tab[i,j]) c++;
            return c;
        }
    }
}