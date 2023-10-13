// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: UserService
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Service for managing users in the Dream Travel Web API
// --------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using DreamTravelWebAPI.Models;
using MongoDB.Driver;

namespace DreamTravelWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        // Constructor: Initializes MongoDB settings and collection
        public UserService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        // Fetches all users
        public List<User> GetAll() => _users.Find(user => true).ToList();

        // Fetches a user by NIC
        public User GetByNic(string nic) => _users.Find<User>(user => user.NIC == nic).FirstOrDefault();

        // Creates a new user
        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        // Updates an existing user by NIC
        public void Update(string nic, User userIn)
        {
            var updateDefinition = Builders<User>.Update
                .Set(u => u.Password, userIn.Password)
                .Set(u => u.IsActive, userIn.IsActive);
            _users.UpdateOne(user => user.NIC == nic, updateDefinition);
        }

        // Deletes a user by NIC
        public void Delete(string nic) => _users.DeleteOne(user => user.NIC == nic);

        // Checks if a user exists by NIC
        public bool Exists(string nic) => _users.CountDocuments(user => user.NIC == nic) > 0;

        // Validates a user's password
        public bool ValidatePassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        // Hashes a user's password
        public void HashPassword(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }

        // Activates a user by NIC
        public void Activate(string nic)
        {
            var user = GetByNic(nic) ?? throw new Exception("User not found.");
            if (user.IsActive)
            {
                throw new Exception("User is already activated.");
            }
            user.IsActive = true;
            Update(nic, user);
        }

        // Deactivates a user by NIC
        public void Deactivate(string nic)
        {
            var user = GetByNic(nic);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            if (user.IsActive == false)
            {
                throw new Exception("User is already deactivated.");
            }
            user.IsActive = false;
            Update(nic, user);
        }
    }
}
