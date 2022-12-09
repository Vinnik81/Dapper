using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_Dapper
{
    public class Cities : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return new Cities
            {
                Id = Id,
                Name = Name
            };
        }

        public override string? ToString()
        {
            base.ToString();
            return ($"{Id}) {Name}");
        }
    }
}
