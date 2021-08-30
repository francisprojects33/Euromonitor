using System;
using System.Collections.Generic;
using System.Text;

namespace EUROMONITOR.Model.DTO
{
    public class SubscriptionDto
    {
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
