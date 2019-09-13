using System;

namespace JobApprovalService.Domain
{
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
}