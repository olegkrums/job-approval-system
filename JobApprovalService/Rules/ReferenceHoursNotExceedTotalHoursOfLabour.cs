using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;
using System.Linq;

namespace JobApprovalService.Rules
{
    public class ReferenceHoursNotExceedTotalHoursOfLabour : IJobApprovalRule
    {
        public JobApprovalDecision EvaluateRule(JobSheet jobSheet)
        {
            var labourHours = jobSheet.Items.Sum(x => x.ItemTime);
            if (jobSheet.ReferenceHoursInMin > labourHours)
                return new JobApprovalDecision(JobApprovalDecisionEnum.Declined, "Reference hours exceed the actual labour hours.");

            return new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
        }
    }
}