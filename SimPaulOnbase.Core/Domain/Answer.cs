using System;

namespace SimPaulOnbase.Core.Domain
{
    public class Answer
    {
        public long IdAnswer { get; set; }
        public DateTimeOffset Created { get; set; }
        public object Value { get; set; }
        public QuestionAlternative QuestionAlternative { get; set; }
    }
}
