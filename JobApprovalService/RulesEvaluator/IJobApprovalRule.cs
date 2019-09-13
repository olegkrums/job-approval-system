using JobApprovalService.Domain;
using System;

namespace JobApprovalService.RulesEvaluator
{
    public interface IJobApprovalRule
    {
        JobApprovalDecision EvaluateRule(JobSheet jobSheet);
    }
}