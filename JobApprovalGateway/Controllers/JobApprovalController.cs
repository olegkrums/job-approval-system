using System;
using System.Collections.Generic;
using JobApprovalService.DataAccess;
using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;
using Microsoft.AspNetCore.Mvc;

namespace JobApprovalGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApprovalController : ControllerBase
    {
        private readonly IRepository<JobSheet> _jobSheets;

        public JobApprovalController(IRepository<JobSheet> jobSheets)
        {
            _jobSheets = jobSheets;
        }

        // GET api/JobApproval/jobSheet
        [HttpPost]
        [Route("ApprovalJobSheet")]
        public JobApprovalDecision ApproveJobSheet([FromBody]JobSheet jobSheet)
        {
            JobRulesEvaluator jobRulesEvaluator = new JobRulesEvaluator();

            return jobRulesEvaluator.EvaluateAllJobRules(jobSheet);
        }

        // GET api/JobApproval/GetJobSheet/2ce22be0-98cc-423b-9446-d7a5cf9e6756
        [HttpGet]
        [Route("GetJobSheet/{jobSheetId}")]
        public JobSheet GetJobSheet(Guid jobSheetId)
        {
            return _jobSheets.GetById(jobSheetId);
        }
    }
}