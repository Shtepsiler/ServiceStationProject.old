﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        [NotMapped]
        public Job Job { get; set; }
        public DateTime IssueDate { get; set; }
        public bool Delivered { get; set; }
        public bool IsOrdered { get; set; }
        [NotMapped]
        public List<OrderPart> OrderParts { get; set; }

    }
}
