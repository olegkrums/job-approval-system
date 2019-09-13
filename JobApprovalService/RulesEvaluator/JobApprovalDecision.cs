namespace JobApprovalService.RulesEvaluator
{
    public class JobApprovalDecision
    {
        public JobApprovalDecisionEnum JobApprovalDecisionEnum { get; private set; }
        public string ReasonForDeclining { get; private set; }

        public JobApprovalDecision() { }

        public JobApprovalDecision(JobApprovalDecisionEnum jobApprovalDecisionEnum, string reasonForDeclining = "")
        {
            JobApprovalDecisionEnum = jobApprovalDecisionEnum;
            ReasonForDeclining = reasonForDeclining;
        }
    }

    public enum JobApprovalDecisionEnum
    {
        Approved = 1,
        Refered = 2,
        Declined = 3
    }
}