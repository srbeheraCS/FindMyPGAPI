using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Cities
{
    public interface ICityService
    {
        void InsertCity(City city);
        void InsertCities(List<City> cities);
        void UpdateCity(City city);
        List<City> GetAllCities();
        List<City> GetAllActiveCities();
        City GetCityById(int id);
        List<City> GetAllActiveCityByStateId(int StateId);
    }
}
