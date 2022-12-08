using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Dapper
{
    public class Countries : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return new Countries
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

    public class Categories : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return new Categories
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

    public interface IMailingsRepository
    {
        void CreateClients(Clients clients);
        void CreateCountrie(Countries countries);
        void CreateCities(Cities cities);
        void CreateCategories(Categories categories);
        void CreatePromotions(Promotions promotions);
        void UpdateClients(Clients clients);
        void UpdateCountries(Countries countries);
        void UpdateCities(Cities cities);
        void UpdateCategories(Categories categories);
        void UpdatePromotions(Promotions promotions);
        void DeleteClients(Clients clients);
        void DeleteCountrie(Countries countries);
        void DeleteCities(Cities cities);
        void DeleteCategories(Categories categories);
        void DeletePromotions(Promotions promotions);
        List<Clients> GetClients();
        List<Clients> GetEmailClients();
        List<Categories> GetCategories();
        List<Promotions> GetPromotions();
        List<Cities> GetCities();
        List<Countries> GetCountries();
        List<Clients> GetClientsFromCity(string city);
        List<Clients> GetClientsFromCountries(string country);
        List<Promotions> GetPromotionsFromCountries(string country);
        List<Cities> GetCitiesForCountry(string country);
        List<Categories> GetCategoriesForClient(string client);
        List<Promotions> GetPromotionsForCategories(string categories);
    }

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
            Console.WriteLine($"Create {promotions.Id}");
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
    class Program
    {
        public static IMailingsRepository repository;
        static void Main(string[] args)
        {
            repository = new MailingsRepository();
            StartServer();
            Console.Read();

            static async Task StartServer()
            {
                var server = new HttpListener();
                server.Prefixes.Add("http://127.0.0.1:8080/");
                server.Start();
                Console.WriteLine("Запуск сервера...");

                while (true)
                {
                    var context = await server.GetContextAsync();
                    using var stream = new StreamWriter(context.Response.OutputStream);
                    var request = context.Request;
                    var response = context.Response;

                    Console.WriteLine($"{request.HttpMethod} - {request.RawUrl} - {DateTime.Now}");

                    if (request.RawUrl == "/favicon.ico")
                    {
                        var bytes = File.ReadAllBytes("screencart.ico");
                        stream.Write(bytes);
                        continue;
                    }


                    if (request.RawUrl.Contains("/Mailings"))
                    {
                        if (request.RawUrl.Contains("CreateCountrie"))
                        {
                            Countries countries = new Countries();
                            countries.Name = request.QueryString["name"];
                            repository.CreateCountrie(countries);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is countries add: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + countries.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("CreateClients"))
                        {
                            Clients clients = new Clients();
                            clients.Id = int.Parse(request.QueryString["id"]);
                            clients.FullName = request.QueryString["fullname"];
                            clients.DateOfBith = DateTime.Parse(request.QueryString["dateofbith"]);
                            clients.Gender = request.QueryString["gender"];
                            clients.Email = request.QueryString["email"];
                            clients.CountryId = int.Parse(request.QueryString["countryid"]);
                            clients.CityId = int.Parse(request.QueryString["cityid"]);
                            repository.UpdateClients(clients);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is clients add: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + clients.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("CreateCities"))
                        {
                            Cities cities = new Cities();
                            cities.Name = request.QueryString["name"];
                            repository.CreateCities(cities);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is cities add: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + cities.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("CreateCategories"))
                        {
                            Categories categories = new Categories();
                            categories.Name = request.QueryString["name"];
                            repository.CreateCategories(categories);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is categories add: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + categories.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("CreatePromotions"))
                        {
                            Promotions promotions = new Promotions();
                            promotions.Percent = int.Parse(request.QueryString["percent"]);
                            promotions.StartDate = DateTime.Parse(request.QueryString["startdate"]);
                            promotions.EndDate = DateTime.Parse(request.QueryString["enddate"]);
                            promotions.CountryId = int.Parse(request.QueryString["countryid"]);
                            promotions.ProducId = int.Parse(request.QueryString["producid"]);
                            repository.CreatePromotions(promotions);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is promotions add: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + promotions.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("UpdateClients"))
                        {
                            Clients clients = new Clients();
                            clients.Id = int.Parse(request.QueryString["id"]);
                            clients.FullName = request.QueryString["fullname"];
                            clients.DateOfBith = DateTime.Parse(request.QueryString["dateofbith"]);
                            clients.Gender = request.QueryString["gender"];
                            clients.Email = request.QueryString["email"];
                            clients.CountryId = int.Parse(request.QueryString["countryid"]);
                            clients.CityId = int.Parse(request.QueryString["cityid"]);
                            repository.UpdateClients(clients);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is clients update: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + clients.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("UpdateCountries"))
                        {
                            Countries countries = new Countries();
                            countries.Id = int.Parse(request.QueryString["id"]);
                            countries.Name = request.QueryString["name"];
                            repository.UpdateCountries(countries);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is countries update: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + countries.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("UpdateCities"))
                        {
                            Cities cities = new Cities();
                            cities.Id = int.Parse(request.QueryString["id"]);
                            cities.Name = request.QueryString["name"];
                            repository.UpdateCities(cities);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is cities update: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + cities.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("UpdateCategories"))
                        {
                            Categories categories = new Categories();
                            categories.Id = int.Parse(request.QueryString["id"]);
                            categories.Name = request.QueryString["name"];
                            repository.UpdateCategories(categories);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is categories update: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + categories.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("UpdatePromotions"))
                        {
                            Promotions promotions = new Promotions();
                            promotions.Id = int.Parse(request.QueryString["id"]);
                            promotions.Percent = int.Parse(request.QueryString["percent"]);
                            promotions.StartDate = DateTime.Parse(request.QueryString["startdate"]);
                            promotions.EndDate = DateTime.Parse(request.QueryString["enddate"]);
                            promotions.CountryId = int.Parse(request.QueryString["countryid"]);
                            promotions.ProducId = int.Parse(request.QueryString["producid"]);
                            repository.UpdatePromotions(promotions);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is promotions update: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + promotions.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("DeleteClients"))
                        {
                            Clients clients = new Clients();
                            clients.Id = int.Parse(request.QueryString["id"]);
                            repository.DeleteClients(clients);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is clients delete: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + clients.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("DeleteCountrie"))
                        {
                            Countries countries = new Countries();
                            countries.Id = int.Parse(request.QueryString["id"]);
                            repository.DeleteCountrie(countries);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is countrie delete: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + countries.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("DeleteCities"))
                        {
                            Cities cities = new Cities();
                            cities.Id = int.Parse(request.QueryString["id"]);
                            repository.DeleteCities(cities);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is city delete: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + cities.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("DeleteCategories"))
                        {
                            Categories categories = new Categories();
                            categories.Id = int.Parse(request.QueryString["id"]);
                            repository.DeleteCategories(categories);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is categorie delete: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + categories.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("DeletePromotions"))
                        {
                            Promotions promotions = new Promotions();
                            promotions.Id = int.Parse(request.QueryString["id"]);
                            repository.DeletePromotions(promotions);
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 stile='color:blue'>This is promotion delete: </h1>");
                            stringBuilder.Append("<h1 style='color:blue'>" + promotions.ToString() + "</h1>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        //-------------------------------------------------------------Dapper Задание2-----------------------------------------------------------------------//

                        else if (request.RawUrl.Contains("GetClients"))
                        {
                            var city = request.QueryString["city"];
                            var country = request.QueryString["country"];
                            if (!string.IsNullOrWhiteSpace(city))
                            {
                                var clients = repository.GetClientsFromCity(city);
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append($"<h1 style = 'color:red'>This is list of clients from city {city}: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Clients item in clients)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.FullName} | {item.Gender}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                            else if (!string.IsNullOrWhiteSpace(country))
                            {
                                var clients = repository.GetClientsFromCountries(country);
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append($"<h1 style = 'color:red'>This is list of clients from country {country}: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Clients item in clients)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.FullName} | {item.Gender}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                            else
                            {
                                var clients = repository.GetClients();
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append("<h1 style = 'color:green'>This is list of clients: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Clients item in clients)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.FullName} | {item.DateOfBith} | {item.Gender} | {item.Email} | {item.CountryId} | {item.CityId}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                        }

                        else if (request.RawUrl.Contains("GetEmailClients"))
                        {
                            var clients = repository.GetClients();
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 style = 'color:green'>This is list of email: </h1>");
                            stringBuilder.Append("<ul>");
                            foreach (Clients item in clients)
                            {
                                stringBuilder.Append($"<li>{item.Id}) {item.FullName} | {item.Email}");
                            }
                            stringBuilder.Append("</ul>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }

                        else if (request.RawUrl.Contains("GetCategories"))
                        {
                            var client = request.QueryString["client"];
                            if (!string.IsNullOrWhiteSpace(client))
                            {
                                var categories = repository.GetCategoriesForClient(client);
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append($"<h1 style = 'color:red'>This is list of categories for client {client}: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Categories item in categories)
                                {
                                    stringBuilder.Append($"<li> {item.Name}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                            else
                            {
                                var categoties = repository.GetCategories();
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append("<h1 style = 'color:green'>This is list of categories: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Categories item in categoties)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.Name}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                        }

                        else if (request.RawUrl.Contains("GetPromotions"))
                        {
                            var country = request.QueryString["country"];
                            var category = request.QueryString["category"];
                            if (!string.IsNullOrWhiteSpace(country))
                            {
                                var promotions = repository.GetPromotionsFromCountries(country);
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append($"<h1 style = 'color:red'>This is list of promotions from country {country}: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Promotions item in promotions)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.Percent} | {item.StartDate} | {item.EndDate} | {item.ProducId}</li>");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                            else if (!string.IsNullOrWhiteSpace(category))
                            {
                                var promotions = repository.GetPromotionsForCategories(category);
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append($"<h1 style = 'color:red'>This is list of promotions for category {category}: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Promotions item in promotions)
                                {
                                    stringBuilder.Append($"<li>{item.Percent} | {item.StartDate} | {item.EndDate} | {item.CountryId} | {item.ProducId}</li>");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                            else
                            {
                                var promotions = repository.GetPromotions();
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append("<h1 style = 'color:green'>This is list of promotions: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Promotions item in promotions)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.Percent} | {item.StartDate} | {item.EndDate} | {item.CountryId} | {item.ProducId}</li>");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                        }

                        else if (request.RawUrl.Contains("GetCities"))
                        {
                            var country = request.QueryString["country"];
                            if (!string.IsNullOrWhiteSpace(country))
                            {
                                var cities = repository.GetCitiesForCountry(country);
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append($"<h1 style = 'color:red'>This is list of cities from {country}: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Cities item in cities)
                                {
                                    stringBuilder.Append($"<li> {item.Name}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }

                            else
                            {
                                var cities = repository.GetCities();
                                StringBuilder stringBuilder = new StringBuilder(1000);
                                stringBuilder.Append("<h1 style = 'color:green'>This is list of cities: </h1>");
                                stringBuilder.Append("<ul>");
                                foreach (Cities item in cities)
                                {
                                    stringBuilder.Append($"<li>{item.Id}) {item.Name}");
                                }
                                stringBuilder.Append("</ul>");
                                await stream.WriteLineAsync(stringBuilder.ToString());
                                continue;
                            }
                        }

                        else if (request.RawUrl.Contains("GetCountries"))
                        {
                            var countries = repository.GetCountries();
                            StringBuilder stringBuilder = new StringBuilder(1000);
                            stringBuilder.Append("<h1 style = 'color:green'>This is list of countries: </h1>");
                            stringBuilder.Append("<ul>");
                            foreach (Countries item in countries)
                            {
                                stringBuilder.Append($"<li>{item.Id}) {item.Name}");
                            }
                            stringBuilder.Append("</ul>");
                            await stream.WriteLineAsync(stringBuilder.ToString());
                            continue;
                        }
                    }
                }
            }
        }
    }
}

