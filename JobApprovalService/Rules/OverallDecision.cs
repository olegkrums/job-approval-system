using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;

namespace JobApprovalService.Rules
{
    public class OverallDecision : IJobApprovalRule
    {
        public JobApprovalDecision EvaluateRule(JobSheet jobSheet)
        {
            // Hard-coded, take as parameter
            var tenPercent = 10;
            var fifteenPercent = 15;
            var tenPercentTotal = ((jobSheet.TotalCost / 100) * tenPercent) + jobSheet.TotalCost;
            var fifteenPercentTotal = ((jobSheet.TotalCost / 100) * fifteenPercent) + jobSheet.TotalCost;

            if (jobSheet.ReferenceTotalPrice < tenPercentTotal)
                return new JobApprovalDecision(JobApprovalDecisionEnum.Approved);

            if (jobSheet.ReferenceTotalPrice > tenPercentTotal && jobSheet.ReferenceTotalPrice <= fifteenPercentTotal)
                return new JobApprovalDecision(JobApprovalDecisionEnum.Refered, "Reference price exceed 10%.");

            if (jobSheet.ReferenceTotalPrice > fifteenPercentTotal)
                return new JobApprovalDecision(JobApprovalDecisionEnum.Declined, "Reference price exceed 15%.");

            return new JobApprovalDecision(JobApprovalDecisionEnum.Declined);
        }
    }
}