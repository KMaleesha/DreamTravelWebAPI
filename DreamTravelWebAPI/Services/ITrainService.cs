using System.Collections.Generic;
using DreamTravelWebAPI.Models;

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
        List<Train> GetByIsPublished(bool isPublished);
        Train GetTrainById(string trainId);
        void DeactivateTrain(string trainId);
        void ActiveTrain(String trainId);
    }
}
