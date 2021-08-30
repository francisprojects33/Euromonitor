using System;
using System.Collections.Generic;
using System.Text;

namespace EUROMONITOR.Model.DTO
{
    public class BookDto
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public decimal Price { get; set; }
    }
}
