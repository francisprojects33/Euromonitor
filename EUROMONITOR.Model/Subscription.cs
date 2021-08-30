using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EUROMONITOR.Model
{
    public class Subscription
    {
        [Key]
        public string Id { get; set; }
        public User User { get; set; }

        [Key]
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public DateTime SubscriptionDate { get; set; }
    }
}
