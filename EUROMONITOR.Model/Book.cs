using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EUROMONITOR.Model
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public IList<Subscription> Subscriptions { get; set; }
    }
}
