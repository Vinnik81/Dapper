using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HomeWork_Dapper
{
    public class MailingsRepository : IMailingsRepository
    {
        readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MailingsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void CreateCategories(Categories categories)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "insert into Categories (Name) values (@Name)";
            db.Execute(query, categories);
        }

        public void CreateCities(Cities cities)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "insert into Cities (Name) values (@Name)";
            db.Execute(query, cities);
        }

        public void CreateClients(Clients clients)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "insert into Clients  (Id ,FullName, DateOfBith, Gender, Email, CountryId, CityId) values (@Id ,@FullName, @DateOfBith, @Gender, @Email, @CountryId,  @CityId)";
            db.Execute(query, clients);
        }

        public void CreateCountrie(Countries countries)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "insert into Countries (Name) values (@Name)";
            db.Execute(query, countries);
        }

        public void CreatePromotions(Promotions promotions)
        {
            using var db = new SqlConnection(connectionString);
            var query = "INSERT INTO Promotions([Percent],[StartDate],[EndDate],CountryId,ProducId) VALUES (@Percent,@startDate,@endDate,@countryId,@productId)";

            var parametrs = new DynamicParameters();
            parametrs.Add("@Percent", promotions.Percent);
            parametrs.Add("@startDate", promotions.StartDate);
            parametrs.Add("@endDate", promotions.EndDate);
            parametrs.Add("@countryId", promotions.CountryId);
            parametrs.Add("@productId", promotions.ProducId);

            db.Execute(query, parametrs);
        }

        public void DeleteCategories(Categories categories)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "delete from Categories where Id = @Id";
            db.Execute(query, categories);
        }

        public void DeleteCities(Cities cities)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "delete from Cities where Id = @Id";
            db.Execute(query, cities);
        }

        public void DeleteClients(Clients clients)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "delete from Clients where Id = @Id";
            db.Execute(query, clients);
        }

        public void DeleteCountrie(Countries countries)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "delete from Countries where Id = @Id";
            db.Execute(query, countries);
        }

        public void DeletePromotions(Promotions promotions)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "delete from Promotions where Id = @Id";
            db.Execute(query, promotions);
        }

        public List<Categories> GetCategories()
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT * FROM Categories";
            var categories = db.Query<Categories>(query);
            return categories.ToList();
        }

        public List<Categories> GetCategoriesForClient(string client)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT ct.Name FROM InterestedBuyers ib JOIN Categories ct ON ib.CategoryId = ct.Id JOIN Clients cl ON ib.ClientId = cl.Id where cl.FullName = @client";
            var categories = db.Query<Categories>(query, new { client });
            return categories.ToList();
        }

        public List<Cities> GetCities()
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT * FROM Cities";
            var cities = db.Query<Cities>(query);
            return cities.ToList();
        }

        public List<Cities> GetCitiesForCountry(string country)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT ci.Name FROM Clients cl JOIN Cities ci ON cl.CityId = ci.Id JOIN Countries co ON cl.CountryId = co.Id where co.Name = @country";
            var cities = db.Query<Cities>(query, new { country });
            return cities.ToList();
        }

        public List<Clients> GetClients()
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT * FROM Clients";
            var clients = db.Query<Clients>(query);
            return clients.ToList();
        }

        public List<Clients> GetClientsFromCity(string city)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT cl.Id, cl.FullName, cl.Gender FROM Clients cl, Cities ci where cl.CityId = ci.Id and ci.Name = @city";
            var clients = db.Query<Clients>(query, new { city });
            return clients.ToList();
        }

        public List<Clients> GetClientsFromCountries(string country)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT cl.Id, cl.FullName, cl.Gender FROM Clients cl, Countries co where cl.CountryId = co.Id and co.Name = @country";
            var clients = db.Query<Clients>(query, new { country });
            return clients.ToList();
        }

        public List<Countries> GetCountries()
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT * FROM Countries";
            var countries = db.Query<Countries>(query);
            return countries.ToList();
        }

        public List<Clients> GetEmailClients()
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT Id, FullName, Email FROM Clients";
            var clients = db.Query<Clients>(query);
            return clients.ToList();
        }

        public List<Promotions> GetPromotions()
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT * FROM Promotions";
            var promotions = db.Query<Promotions>(query);
            return promotions.ToList();

        }

        public List<Promotions> GetPromotionsForCategories(string categories)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT pr.[Percent], pr.StartDate, pr.EndDate, pr.CountryId, pr.ProducId FROM Promotions pr JOIN Products p ON pr.ProducId = p.Id JOIN Categories c ON p.CategoryId = c.Id where c.Name = @categories";
            var promotions = db.Query<Promotions>(query, new { categories });
            return promotions.ToList();
        }

        public List<Promotions> GetPromotionsFromCountries(string country)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "SELECT pr.Id, pr.[Percent], pr.StartDate, pr.EndDate, pr.ProducId FROM Promotions pr, Countries co, Products p where pr.CountryId = co.Id and pr.ProducId = p.Id and co.Name = @country";
            var promotions = db.Query<Promotions>(query, new { country });
            return promotions.ToList();
        }

        public void UpdateCategories(Categories categories)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "update Categories set Name = @Name where Id = @Id";
            db.Execute(query, categories);
        }

        public void UpdateCities(Cities cities)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "update Cities set Name = @Name where Id = @Id";
            db.Execute(query, cities);
        }

        public void UpdateClients(Clients clients)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "update Clients set FullName = @FullName, DateOfBith = @DateOfBith, Gender = @Gender, Email = @Email, CountryId = @CountryId, CityId = @CityId where Id = @Id";
            db.Execute(query, clients);
        }

        public void UpdateCountries(Countries countries)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "update Countries set Name = @Name where Id = @Id";
            db.Execute(query, countries);
        }

        public void UpdatePromotions(Promotions promotions)
        {
            using var db = new SqlConnection(connectionString);
            db.Open();
            var query = "update Promotions set [Percent] = @Percent, StartDate = @StartDate, EndDate = @EndDate, CountryId = @CountryId, ProducId = @ProducId where Id = @Id";
            db.Execute(query, promotions);
        }
    }
}
