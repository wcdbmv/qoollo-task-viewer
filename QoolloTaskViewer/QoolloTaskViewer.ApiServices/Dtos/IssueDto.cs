using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Dtos
{
    public enum Difficulty
    {
        Unrecognized,
        Easy,
        Medium,
        Hard
    }

    public enum Priority
    {
        Unrecognized,
        Low,
        Medium,
        High
    }

    public enum State
    {
        Open,
        Closed
    }

    public class IssueDto
    {
        public string Name { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public Difficulty Difficulty { get; set; }
        public Priority Priority { get; set; }
        public List<LabelDto> Labels { get; set; }
        public ServiceInfoDto ServiceInfo { get; set; }
    }
}
