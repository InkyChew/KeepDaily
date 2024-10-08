﻿using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;

namespace RepoLayer.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<User> _users;

        public UserRepo(KeepDailyContext db)
        {
            _db = db;
            _users = db.AppUser;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _users;
        }

        public User? GetUser(int id)
        {
            return _users.SingleOrDefault(_ => _.Id == id);
        }

        public User? GetUser(string email)
        {
            return _users.SingleOrDefault(_ => _.Email == email);
        }

        public User InsertUser(User user)
        {
            _users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User FindUser(int id)
        {
            return _users.Find(id) ?? throw new KeyNotFoundException($"User(Id:{id}) does not exist.");
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void InActiveUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
