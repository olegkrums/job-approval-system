using JobApprovalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApprovalService.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private List<JobSheet> _jobSheets;
        private readonly decimal _labourCostHour = 45;

        public Repository()
        {
            _jobSheets = new List<JobSheet>();

            var tyreOne = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre",
                ItemTime = 30
            };

            var tyreTwo = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre",
                ItemTime = 30
            };

            var tyreThree = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre",
                ItemTime = 30
            };

            var tyreFour = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre",
                ItemTime = 30
            };

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

            var oil5Liters = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Oil",
                UnitCost = 20,
                ItemName = "Oil 5 Lt",
                ItemTime = 30
            };

            var oil10Liters = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Oil",
                UnitCost = 40,
                ItemName = "Oil 10 Lt",
                ItemTime = 30
            };

            var exhaust = new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Exhaust",
                UnitCost = 175,
                ItemName = "Exhaust",
                ItemTime = 240
            };

            var tyresBreakPadsDiscs = new List<Item>
            {
                tyreOne,
                tyreTwo,
                tyreThree,
                breakPads,
                discs
            };

            var js1= new JobSheet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ReferenceHoursInMin = tyresBreakPadsDiscs.Sum(x => x.ItemTime),
                ReferenceTotalPrice = (_labourCostHour * tyresBreakPadsDiscs.Sum(x => x.ItemTime) / 60) + (tyresBreakPadsDiscs.Sum(x => x.UnitCost)),
                LaborHourCost = _labourCostHour,
                Items = tyresBreakPadsDiscs
            };

            var exhaustOilTyres = new List<Item>
            {
                exhaust,
                oil5Liters,
                tyreOne,
                tyreTwo
            };

            var js2 = new JobSheet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ReferenceHoursInMin = exhaustOilTyres.Sum(x=>x.ItemTime),
                ReferenceTotalPrice = (_labourCostHour * exhaustOilTyres.Sum(x => x.ItemTime) / 60) + (exhaustOilTyres.Sum(x => x.UnitCost)),
                LaborHourCost = _labourCostHour,
                Items = exhaustOilTyres
            };

            var breakPadsDiscsOil5LitersTyres = new List<Item>
            {
                breakPads,
                discs,
                oil5Liters,
                tyreOne,
                tyreTwo
            };

            var js3 = new JobSheet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ReferenceHoursInMin = breakPadsDiscsOil5LitersTyres.Sum(x => x.ItemTime),
                ReferenceTotalPrice = (_labourCostHour * breakPadsDiscsOil5LitersTyres.Sum(x => x.ItemTime) / 60) + (breakPadsDiscsOil5LitersTyres.Sum(x => x.UnitCost)),
                LaborHourCost = _labourCostHour,
                Items = breakPadsDiscsOil5LitersTyres
            };

            _jobSheets.Add(js1);
            _jobSheets.Add(js2);
            _jobSheets.Add(js3);
        }

        public IEnumerable<T> GetAll()
        {
            return (IEnumerable<T>)_jobSheets;
        }

        public T GetById(Guid Id)
        {
           return _jobSheets.Find(x => x.Id == Id) as T;
        }
    }
}