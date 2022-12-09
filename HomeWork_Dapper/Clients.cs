using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_Dapper
{
    public class Clients : ICloneable
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBith { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

        public object Clone()
        {
            return new Clients
            {
                Id = Id,
                FullName = FullName,
                DateOfBith = DateOfBith,
                Gender = Gender,
                Email = Email,
                CountryId = CountryId,
                CityId = CityId
            };
        }

        public override string ToString()
        {
            base.ToString();
            return ($"{Id}) {FullName}, {DateOfBith}, {Gender}, {Email}, {CountryId}, {CityId}");
        }
    }
}
