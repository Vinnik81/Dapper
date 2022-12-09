using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_Dapper
{
    public class Promotions : ICloneable
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CountryId { get; set; }
        public int ProducId { get; set; }

        public object Clone()
        {
            return new Promotions
            {
                Id = Id,
                Percent = Percent,
                StartDate = StartDate,
                EndDate = EndDate,
                CountryId = CountryId,
                ProducId = ProducId
            };
        }

        public override string ToString()
        {
            base.ToString();
            return ($"{Id}) {Percent}%, {StartDate}, {EndDate}, {CountryId}, {ProducId}");
        }
    }
}
