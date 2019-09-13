using JobApprovalService.Domain;
using JobApprovalService.RulesEvaluator;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class JobApprovalServiceTests
    {
        private JobSheet _jobSheet;

        [SetUp]
        public void Setup()
        {
            _jobSheet = new JobSheet
            {
                Id = Guid.Parse("2ce22be0-98cc-423b-9446-d7a5cf9e6756"),
                ReferenceHoursInMin = new Random().Next(5, 9),
                ReferenceTotalPrice = new Random().Next(500, 1000),
                LaborHourCost = 45,
                Items = new List<Item>()
            };
        }

        [Test]
        public void BreakPadsAndDiscsWereChangedAtTheSameTime()
        {
            var breakPads = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Pads",
                UnitCost = 50,
                ItemName = "Pads",
                ItemTime = 60
            };

            var discs = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Discs",
                UnitCost = 90,
                ItemName = "Discs",
                ItemTime = 100
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(breakPads);
            _jobSheet.Items.Add(discs);


            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateBreakPadsAndDiscsChangeTogetherRule(_jobSheet).JobApprovalDecisionEnum);
            Assert.AreEqual(jobApprovalDecision.ReasonForDeclining, jobRulesEvaluatorMock.Object.EvaluateBreakPadsAndDiscsChangeTogetherRule(_jobSheet).ReasonForDeclining);
        }

        [Test]
        public void BreakPadsAndDiscsWereNotChangedAtTheSameTime()
        {
            var breakPads = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Pads",
                UnitCost = 50,
                ItemName = "Pads",
                ItemTime = 60
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(breakPads);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Declined);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateBreakPadsAndDiscsChangeTogetherRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void OneExhaust()
        {
            var exhaustOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Exhaust",
                UnitCost = 175,
                ItemName = "Exhaust 1",
                ItemTime = 240
            };


            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(exhaustOne);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateOneExhaustRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void ExceedingOneExhaust()
        {
            var exhaustOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Exhaust",
                UnitCost = 175,
                ItemName = "Exhaust 1",
                ItemTime = 240
            };

            var exhaustTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Exhaust",
                UnitCost = 175,
                ItemName = "Exhaust 2",
                ItemTime = 240
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(exhaustOne);
            _jobSheet.Items.Add(exhaustTwo);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Declined);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateOneExhaustRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void ReferenceHoursNotExceedTotalHoursOfLabour()
        {
            var pads = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Pads",
                UnitCost = 50,
                ItemName = "Pads",
                ItemTime = 60
            };

            var discs = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Discs",
                UnitCost = 90,
                ItemName = "Discs",
                ItemTime = 160
            };

            _jobSheet.ReferenceHoursInMin = 220;
            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(pads);
            _jobSheet.Items.Add(discs);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateReferenceHoursNotExceedTotalHoursOfLabourRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void ReferenceHoursExceedTotalHoursOfLabour()
        {
            var pads = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Pads",
                UnitCost = 50,
                ItemName = "Pads",
                ItemTime = 60
            };

            var discs = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Discs",
                UnitCost = 90,
                ItemName = "Discs",
                ItemTime = 160
            };

            _jobSheet.ReferenceHoursInMin = 221;
            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(pads);
            _jobSheet.Items.Add(discs);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Declined);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateReferenceHoursNotExceedTotalHoursOfLabourRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void TyresChangedInPairsAndMaxFour()
        {
            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre One",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Two",
                ItemTime = 30
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(tyreOne);
            _jobSheet.Items.Add(tyreTwo);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateTyresChangedInPairsAndMaxFourRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void TyresChangedNotPairsAndMaxFour()
        {
            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre One",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Two",
                ItemTime = 30
            };

            var tyreThree = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Three",
                ItemTime = 30
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(tyreOne);
            _jobSheet.Items.Add(tyreTwo);
            _jobSheet.Items.Add(tyreThree);

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Declined);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateTyresChangedInPairsAndMaxFourRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void ApproveOverallDecisionUnder10Percent()
        {
            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre One",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Two",
                ItemTime = 30
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(tyreOne);
            _jobSheet.Items.Add(tyreTwo);

            var underTenPercent = (_jobSheet.TotalCost / 100) * (decimal)9.9;
            _jobSheet.ReferenceTotalPrice = _jobSheet.TotalCost + underTenPercent;

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Approved);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateOverallDecisionRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void ReferOverallDecisionAt12Percent()
        {
            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre One",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Two",
                ItemTime = 30
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(tyreOne);
            _jobSheet.Items.Add(tyreTwo);

            var twelvePercent = (_jobSheet.TotalCost / 100) * 12;
            _jobSheet.ReferenceTotalPrice = _jobSheet.TotalCost + twelvePercent;

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Refered);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateOverallDecisionRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void ReferOverallDecisionAt15Percent()
        {
            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre One",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Two",
                ItemTime = 30
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(tyreOne);
            _jobSheet.Items.Add(tyreTwo);

            var twelvePercent = (_jobSheet.TotalCost / 100) * 15;
            _jobSheet.ReferenceTotalPrice = _jobSheet.TotalCost + twelvePercent;

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Refered);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateOverallDecisionRule(_jobSheet).JobApprovalDecisionEnum);
        }

        [Test]
        public void DeclineOverallDecisionAbove15Percent()
        {
            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre One",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre Two",
                ItemTime = 30
            };

            _jobSheet.Items = new List<Item>();
            _jobSheet.Items.Add(tyreOne);
            _jobSheet.Items.Add(tyreTwo);

            var twelvePercent = (_jobSheet.TotalCost / 100) * (decimal)15.1;
            _jobSheet.ReferenceTotalPrice = _jobSheet.TotalCost + twelvePercent;

            var jobApprovalDecision = new JobApprovalDecision(JobApprovalDecisionEnum.Declined);
            var jobRulesEvaluatorMock = new Mock<JobRulesEvaluator>() { CallBase = true };

            Assert.AreEqual(jobApprovalDecision.JobApprovalDecisionEnum, jobRulesEvaluatorMock.Object.EvaluateOverallDecisionRule(_jobSheet).JobApprovalDecisionEnum);
        }
    }
}