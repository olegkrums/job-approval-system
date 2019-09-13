using JobApprovalService.Domain;

namespace JobApprovalService.RulesEvaluator
{
    public interface IJobRulesEvaluator
    {
        JobApprovalDecision EvaluateAllJobRules(JobSheet jobSheet);
        JobApprovalDecision EvaluateBreakPadsAndDiscsChangeTogetherRule(JobSheet jobSheet);
        JobApprovalDecision EvaluateOneExhaustRule(JobSheet jobSheet);
        JobApprovalDecision EvaluateOverallDecisionRule(JobSheet jobSheet);
        JobApprovalDecision EvaluateReferenceHoursNotExceedTotalHoursOfLabourRule(JobSheet jobSheet);
        JobApprovalDecision EvaluateTyresChangedInPairsAndMaxFourRule(JobSheet jobSheet);

    }
}