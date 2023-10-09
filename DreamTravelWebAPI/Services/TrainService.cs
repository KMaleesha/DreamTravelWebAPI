using DreamTravelWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class TrainService : ITrainService
{
    private static List<Train> trains = new List<Train>();

    public Train Create(Train train)
    {
        trains.Add(train);
        return train;
    }

    public Train Update(int id, Train updatedTrain)
    {
        var train = trains.FirstOrDefault(t => t.Id == id);
        if (train == null)
        {
            throw new Exception($"Train with id {id} not found.");
        }

        if (!train.IsPublished)
        {
            train.Name = updatedTrain.Name;
            return train;
        }

        throw new Exception("Cannot update a published train.");
    }

    public bool Delete(int id)
    {
        var train = trains.FirstOrDefault(t => t.Id == id);
        if (train == null)
        {
            throw new Exception($"Train with id {id} not found.");
        }

        if (!train.IsPublished)
        {
            trains.Remove(train);
            return true;
        }

        throw new Exception("Cannot delete a train with reservations or if it's published.");
    }

    public IEnumerable<Train> GetAll()
    {
        return trains;
    }

    public Train GetById(int id)
    {
        var train = trains.FirstOrDefault(t => t.Id == id);
        if (train == null)
        {
            throw new Exception($"Train with id {id} not found.");
        }

        return train;
    }
}
