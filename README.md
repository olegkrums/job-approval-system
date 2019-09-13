# Simple Job Approval System


## General Architecture
Simple Job Approval System's architecture will suite for a well structured Monolith which later could be refactored into more distributed, scalable system.

## Job Approval .Net Core API
JobApprovalController reference from **Job Approval .Net Core Service**:
```csharp
 private readonly IRepository<JobSheet> _jobSheets;
```

 JobApprovalController has two endpoitns:
 - GetJobSheet(Guid jobSheetId)
 - ApproveJobSheet([FromBody]JobSheet jobSheet)

## Job Approval .Net Core Service
- Data Access - Uses generic Repository pattern, which allows to test functionality without the datastore.
- Domain Objects - very difficult to design Domain Model without understanding business domain :)). ReferenceHoursInMin, ReferenceTotalPrice and LaborHourCost might don't belong in JobSheet, not sure.
```csharp
public class JobSheet
    {
        public Guid Id { get; set; }
        public IList<Item> Items { get; set; }
        public int ReferenceHoursInMin { get; set; }
        public decimal ReferenceTotalPrice { get; set; }
        public decimal LaborHourCost { get; set; }
        public decimal TotalCost
        {
            get => (LaborHourCost * Items.Sum(x => x.ItemTime)/60) + (Items.Sum(x => x.UnitCost));
        }
    }
```
GenericCategory property will allow to identify type of Item/Part in the rule evaluator. I'm guessing in real life GenericCategory would be a type that will hold Parent/Child categories.
```csharp
    public class Item
    {
        public Guid ItemId { get; set; }
        public string GenericCategory { get; set; }
        public string ItemName { get; set; }
        /// <summary>
        /// Minutes takes to change an Item
        /// </summary>
        public int ItemTime { get; set; }
        public decimal UnitCost { get; set; }
    }
```

- Rules - 4 business rules + 3 from Overal Decision. All rules return: 
```csharp
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
```
- RulesEvaluator - design is based on the Rule Pattern. This allows one to implement independent rules which is much easier to read then deeply nested boolean logic and test it. 
## Angular 8 SPA
- App Component = Simple Job Approval System
-  JobApprovalApi - makes API calls to **Job Approval .Net Core API** 