using System;
using System.Collections.Generic;

#nullable disable

namespace ENSEK.Data.Access.Entities
{
    public partial class MeterReading
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }

        public virtual Account Account { get; set; }
    }
}
