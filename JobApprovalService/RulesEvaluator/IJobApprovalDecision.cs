namespace JobApprovalService.RulesEvaluator
{
    public interface IJobApprovalDecision
    {
        JobApprovalDecisionEnum JobApprovalDecisionEnum { get; set; }
        string ReasonForDeclining { get; set; }
    }
}