using DreamTravelWebAPI.Models;
using System.Collections.Generic;

namespace DreamTravelWebAPI.Services
{
    public interface ITravelerService
    {
        List<Traveler> GetAll();
        Traveler GetByNic(string nic);
        Traveler Create(Traveler traveler);
        void Update(string nic, Traveler traveler);
        void Delete(string nic);
        bool Exists(string nic);
        void Activate(string nic);
        void Deactivate(string nic);
    }
}
