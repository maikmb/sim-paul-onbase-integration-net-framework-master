using System.Linq;

namespace SimPaulOnbase.Core.Domain
{
    public class Suitability
    {
        public Profile Profile { get; set; }
        public Answer[] Answers { get; set; }

        public SuitabilityAlternative GetSutiabilityAlternativeByQuestionId(long questionId)
        {
            var suitabilityAlternative = this.Answers.FirstOrDefault(args => args.QuestionAlternative?.SuitabilityAlternative?.Id == questionId);
            return suitabilityAlternative.QuestionAlternative.SuitabilityAlternative;
        }
    }
}
