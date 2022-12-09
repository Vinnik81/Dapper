using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_Dapper
{
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
}
