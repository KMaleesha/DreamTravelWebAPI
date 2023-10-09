using DreamTravelWebAPI.Models;
using System.Collections.Generic;

public interface ITrainService
{
    Train Create(Train train);
    Train Update(int id, Train updatedTrain);
    bool Delete(int id);
    IEnumerable<Train> GetAll();
    Train GetById(int id);
}

