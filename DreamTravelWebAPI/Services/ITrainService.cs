using System;
using System.Collections.Generic;

namespace DreamTravelWebAPI.Services
{
    public interface ITrainService
    {
        List<Train> GetAll();
        Train GetById(string id);
        Train Create(Train train);
        void Update(string id, Train train);
        void Delete(string id);
        bool Exists(string id);
        void Activate(string id);
        void Deactivate(string id);
    }
}
