using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApprovalService.Domain
{
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
}