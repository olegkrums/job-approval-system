using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;
using System.Linq;

namespace JobApprovalService.Rules
{
    public class OneExhaust : IJobApprovalRule
    {
        public JobApprovalDecision EvaluateRule(JobSheet jobSheet)
        {
            var exhaust = jobSheet.Items.Where(x => x.GenericCategory == "Exhaust").ToArray();
            

            if (exhaust.Any())
            {
                if (exhaust.Count() > 1)
                    return new JobApprovalDecision(JobApprovalDecisionEnum.Declined, "More than one Exhaust.");
            }

            return new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
        }
    }
}