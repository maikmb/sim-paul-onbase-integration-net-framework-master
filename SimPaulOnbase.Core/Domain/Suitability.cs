using System;
using System.Linq;

namespace SimPaulOnbase.Core.Domain
{
    public class Suitability
    {
        public Profile Profile { get; set; }
        public Answer[] Answers { get; set; }

        public SuitabilityAlternative GetSutiabilityAlternativeByQuestionId(long questionId)
        {
            try
            {
                var suitabilityAlternative = this.Answers.First(args => args.QuestionAlternative?.SuitabilityQuestion?.Id == questionId);
                return suitabilityAlternative.QuestionAlternative.SuitabilityAlternative;
            }
            catch
            {
                return null;
            }
        }

        public bool HasForManySutiabilityAlternative(long questionId, long suitabilityAnternative)
        {
            var suitabilityAlternative = this.Answers.Any(args => args.QuestionAlternative?.SuitabilityQuestion?.Id == questionId &&
                args.QuestionAlternative?.SuitabilityAlternative?.Id == suitabilityAnternative);
            return suitabilityAlternative;
        }

        public SuitabilityAlternative GetForManySutiabilityAlternativeByQuestionId(long questionId, long suitabilityAnternative)
        {
            try
            {
                var suitabilityAlternative = this.Answers.First(args => args.QuestionAlternative?.SuitabilityQuestion?.Id == questionId &&
                    args.QuestionAlternative?.SuitabilityAlternative?.Id == suitabilityAnternative);

                return suitabilityAlternative.QuestionAlternative.SuitabilityAlternative;
            }
            catch
            {
                throw new Exception("Question not found");
            }
        }
    }
}
