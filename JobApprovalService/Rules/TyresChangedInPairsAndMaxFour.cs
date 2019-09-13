using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;
using System.Linq;

namespace JobApprovalService.Rules
{
    public class TyresChangedInPairsAndMaxFour : IJobApprovalRule
    {
        public JobApprovalDecision EvaluateRule(JobSheet jobSheet)
        {
            var tyres = jobSheet.Items.Where(x => x.GenericCategory == "Tyres").ToArray();
            

            if (tyres.Any())
            {
                var noOfTyres = tyres.Count();
                var notInPair = (noOfTyres % 2) != 0;

                if (notInPair || noOfTyres > 4)
                    return new JobApprovalDecision(JobApprovalDecisionEnum.Declined, "Tyres are either not in Pairs or more than 4.");
            }

            return new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
        }
    }
}