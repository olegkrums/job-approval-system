using JobApprovalService.Domain;
using JobApprovalService.Rules;
using System.Collections.Generic;

namespace JobApprovalService.RulesEvaluator
{
    public class JobRulesEvaluator : IJobRulesEvaluator
    {
        List<IJobApprovalRule> _rules = new List<IJobApprovalRule>();

        public JobApprovalDecision EvaluateAllJobRules(JobSheet jobSheet)
        {
            _rules.Add(new TyresChangedInPairsAndMaxFour());
            _rules.Add(new BreakPadsAndDiscsChangeTogether());
            _rules.Add(new OneExhaust());
            _rules.Add(new ReferenceHoursNotExceedTotalHoursOfLabour());
            _rules.Add(new OverallDecision());

            JobApprovalDecision evalResult = new JobApprovalDecision();

            foreach (var rule in _rules)
            {
                evalResult = rule.EvaluateRule(jobSheet);

                if (evalResult.JobApprovalDecisionEnum == JobApprovalDecisionEnum.Declined)
                    return evalResult;
            }

            return evalResult;
        }

        public JobApprovalDecision EvaluateBreakPadsAndDiscsChangeTogetherRule(JobSheet jobSheet)
        {
            IJobApprovalRule rules = new BreakPadsAndDiscsChangeTogether();
            return rules.EvaluateRule(jobSheet);
        }

        public JobApprovalDecision EvaluateOneExhaustRule(JobSheet jobSheet)
        {
            IJobApprovalRule rules = new OneExhaust();
            return rules.EvaluateRule(jobSheet);
        }

        public JobApprovalDecision EvaluateOverallDecisionRule(JobSheet jobSheet)
        {
            IJobApprovalRule rules = new OverallDecision();
            return rules.EvaluateRule(jobSheet);
        }

        public JobApprovalDecision EvaluateReferenceHoursNotExceedTotalHoursOfLabourRule(JobSheet jobSheet)
        {
            IJobApprovalRule rules = new ReferenceHoursNotExceedTotalHoursOfLabour();
            return rules.EvaluateRule(jobSheet);
        }

        public JobApprovalDecision EvaluateTyresChangedInPairsAndMaxFourRule(JobSheet jobSheet)
        {
            IJobApprovalRule rules = new TyresChangedInPairsAndMaxFour();
            return rules.EvaluateRule(jobSheet);
        }
    }
}