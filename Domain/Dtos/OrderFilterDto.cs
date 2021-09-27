using System;

namespace Domain.Dtos
{
    public class OrderFilterDto
    {
        public Guid? OperatorId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string FieldToSortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
