using JobApprovalService.Domain;

namespace JobApprovalService.RulesEvaluator
{
    public interface IJobApprovalRule
    {
        JobApprovalDecision EvaluateRule(JobSheet jobSheet);
    }
}