using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;
using System.Linq;

namespace JobApprovalService.Rules
{
    public class BreakPadsAndDiscsChangeTogether : IJobApprovalRule
    {
        public JobApprovalDecision EvaluateRule(JobSheet jobSheet)
        {
            var breakPads = jobSheet.Items.Where(x => x.GenericCategory == "Break Pads").ToArray();
            var discs = jobSheet.Items.Where(x => x.GenericCategory == "Discs").ToArray();

            var missingPads = (!breakPads.Any() && discs.Any());
            var missingDiscs = (breakPads.Any() && !discs.Any());

            if (missingPads || missingDiscs)
                return new JobApprovalDecision(JobApprovalDecisionEnum.Declined, "Missing pads or discs.");

            return new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
        }
    }
}