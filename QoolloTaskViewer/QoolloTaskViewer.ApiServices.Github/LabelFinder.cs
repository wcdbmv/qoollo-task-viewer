using System;
using System.Collections.Generic;
using System.Text;
using QoolloTaskViewer.ApiServices.Dtos;

namespace QoolloTaskViewer.ApiServices.Github
{
    class LabelFinder
    {
        private readonly List<LabelDto> _labels;

        public LabelFinder(List<LabelDto> labels)
        {
            _labels = labels;
        }

        private Difficulty ChooseDifficulty(string label)
        {
            Difficulty difficulty = Difficulty.Unrecognized;

            if (System.Text.RegularExpressions.Regex.IsMatch(label, "easy", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                difficulty = Difficulty.Easy;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(label, "medium", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                difficulty = Difficulty.Medium;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(label, "hard", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                difficulty = Difficulty.Hard;
            }

            return difficulty;
        }

        public Difficulty GetDifficulty()
        {
            Difficulty difficulty = Difficulty.Unrecognized;

            foreach (var label in _labels)
            {
                string name = label.name.ToLower();
                if (System.Text.RegularExpressions.Regex.IsMatch(name, "difficulty", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    difficulty = ChooseDifficulty(name);
                }
            }

            return difficulty;
        }

        private Priority ChoosePriority(string label)
        {
            Priority priority = Priority.Unrecognized;

            if (System.Text.RegularExpressions.Regex.IsMatch(label, "low", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                priority = Priority.Low;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(label, "medium", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                priority = Priority.Medium;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(label, "high", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                priority = Priority.High;
            }

            return priority;
        }

        public Priority GetPriority()
        {
            Priority priority = Priority.Unrecognized;

            foreach (var label in _labels)
            {
                string name = label.name.ToLower();
                if (System.Text.RegularExpressions.Regex.IsMatch(name, "priority", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    priority = ChoosePriority(name);
                }
            }

            return priority;
        }
    }
}
