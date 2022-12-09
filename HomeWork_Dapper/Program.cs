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
                                    stringBuilder.Append($"<li>{item.Percent} | {item.StartDate} | {item.EndDate} | {item.CountryId} | {item.ProducId}  </li>");
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

