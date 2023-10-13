using System.Collections.Generic;
using DreamTravelWebAPI.Models;

namespace DreamTravelWebAPI.Services
{
    // Interface that defines the contract for Train related services.
    public interface ITrainService
    {
        // Retrieves all trains in the system.
        List<Train> GetAll();

        // Retrieves a specific train by its unique identifier.
        Train GetById(string id);

        // Creates a new train entry.
        Train Create(Train train);

        // Updates the details of an existing train by its identifier.
        void Update(string id, Train train);

        // Deletes a specific train by its identifier.
        void Delete(string id);

        // Checks if a train with the specified identifier exists.
        bool Exists(string id);

        // Retrieves all trains based on their published status.
        List<Train> GetByIsPublished(bool isPublished);

        // Retrieves a train by its identifier (Alternative method to GetById).
        Train GetTrainById(string trainId);

        // Deactivates a train, making it unavailable for operations.
        void DeactivateTrain(string trainId);

        // Activates a previously deactivated train.
        void ActiveTrain(String trainId);
    }
}
