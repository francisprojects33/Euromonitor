using System;
using System.Collections.Generic;
using System.Text;

namespace EUROMONITOR.Model.DTO
{
    public class SubscriptionCreationDto
    {
        public string UserId { get; set; }
        public string BookId { get; set; }
        public DateTime SubscriptionDate => DateTime.Now;
    }
}
