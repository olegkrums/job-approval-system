using JobApprovalService.Domain;
using System;
using System.Collections.Generic;

namespace JobApprovalService.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private List<JobSheet> _jobSheets;

        public Repository()
        {
            _jobSheets = new List<JobSheet>();

            var units = new List<Item>();

            units.Add(new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Tyres",
                UnitCost = 200,
                ItemName = "Tyre",
                ItemTime = 30
            });

            units.Add(new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Pads",
                UnitCost = 50,
                ItemName = "Pads",
                ItemTime = 60
            });

            units.Add(new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Break Discs",
                UnitCost = 90,
                ItemName = "Discs",
                ItemTime = 100
            });

            units.Add(new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Oil",
                UnitCost = 20,
                ItemName = "Discs",
                ItemTime = 30
            });

            units.Add(new Item
            {
                ItemId = Guid.NewGuid(),
                GenericCategory = "Exhaust",
                UnitCost = 175,
                ItemName = "Exhaust",
                ItemTime = 240
            });

            _jobSheets.Add(new JobSheet
            {
                Id = Guid.Parse("2ce22be0-98cc-423b-9446-d7a5cf9e6756"),
                ReferenceHoursInMin = new Random().Next(5, 9),
                ReferenceTotalPrice = new Random().Next(500, 1000),
                LaborHourCost = 45,
                Items = units
            });
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