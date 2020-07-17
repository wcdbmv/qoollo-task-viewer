using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Enums;

namespace QoolloTaskViewer.ApiServices.Github
{
    class LabelFinder
    {
        private readonly List<LabelDto> _labels;

        private static readonly Dictionary<Difficulty, List<string>> difficultyLabels = new Dictionary<Difficulty, List<string>>
        {
            { Difficulty.Easy, new List<string> {"easy", "simple"} },
            { Difficulty.Medium, new List<string> {"medium", "normal", "average"} },
            { Difficulty.Hard, new List<string> {"hard", "difficult" } },
        };
        private readonly Dictionary<Priority, List<string>> priorityLabels = new Dictionary<Priority, List<string>>
        {
            { Priority.Low, new List<string> {"low", "small", "little"} },
            { Priority.Medium, new List<string> {"middle"} },
            { Priority.High, new List<string> { "high", "top"} },
        };

        private readonly Regex easyDifficultyExpression;
        private readonly Regex mediumyDifficultyExpression;
        private readonly Regex hardDifficultyExpression;

        private readonly Regex lowPriorityExpression;
        private readonly Regex mediumPriorityExpression;
        private readonly Regex highPriorityExpression;

        public LabelFinder(List<LabelDto> labels)
        {
            _labels = labels;

            easyDifficultyExpression = new Regex(string.Join("|", difficultyLabels[Difficulty.Easy].Select(Regex.Escape).ToArray()),
            RegexOptions.Singleline | RegexOptions.Compiled);
            mediumyDifficultyExpression = new Regex(string.Join("|", difficultyLabels[Difficulty.Medium].Select(Regex.Escape).ToArray()),
            RegexOptions.Singleline | RegexOptions.Compiled);
            hardDifficultyExpression = new Regex(string.Join("|", difficultyLabels[Difficulty.Hard].Select(Regex.Escape).ToArray()),
            RegexOptions.Singleline | RegexOptions.Compiled);

            lowPriorityExpression = new Regex(string.Join("|", priorityLabels[Priority.Low].Select(Regex.Escape).ToArray()),
            RegexOptions.Singleline | RegexOptions.Compiled);
            mediumPriorityExpression = new Regex(string.Join("|", priorityLabels[Priority.Medium].Select(Regex.Escape).ToArray()),
            RegexOptions.Singleline | RegexOptions.Compiled);
            highPriorityExpression = new Regex(string.Join("|", priorityLabels[Priority.High].Select(Regex.Escape).ToArray()),
            RegexOptions.Singleline | RegexOptions.Compiled);
        }

        public Difficulty GetDifficulty()
        {
            Difficulty difficulty = Difficulty.Unrecognized;

            foreach (var label in _labels)
            {
                string name = label.name.ToLower();
                if (easyDifficultyExpression.Match(name).Success)
                {
                    difficulty = Difficulty.Easy;
                }
                else if (mediumyDifficultyExpression.Match(name).Success)
                {
                    difficulty = Difficulty.Medium;

                }
                else if (hardDifficultyExpression.Match(name).Success)
                {
                    difficulty = Difficulty.Hard;
                }
            }

            return difficulty;
        }

        public Priority GetPriority()
        {
            Priority priority = Priority.Unrecognized;

            foreach (var label in _labels)
            {
                string name = label.name.ToLower();
                if (lowPriorityExpression.Match(name).Success)
                {
                    priority = Priority.Low;
                }
                else if (mediumPriorityExpression.Match(name).Success)
                {
                    priority = Priority.Medium;
                }
                else if (highPriorityExpression.Match(name).Success)
                {
                    priority = Priority.High;
                }
            }

            return priority;
        }
    }
}
