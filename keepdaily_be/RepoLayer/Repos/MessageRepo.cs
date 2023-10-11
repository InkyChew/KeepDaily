using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;

namespace RepoLayer.Repos
{
    public class MessageRepo : IMessageRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<Message> _messages;

        public MessageRepo(KeepDailyContext db)
        {
            _db = db;
            _messages = db.Message;
        }

        public IEnumerable<Message> GetAllMessage()
        {
            return _messages;
        }


        public IEnumerable<Message> GetAllUserMessage(int uid)
        {
            return _messages.Where(_ => _.UserId == uid).OrderByDescending(_ => _.CreatedTime);
        }

        public void InsertMessage(Message message)
        {
            _messages.Add(message);
        }

        public void UpdateMessage(Message message)
        {
            _messages.Update(message);
        }

        public void DeleteMessage(int id)
        {
            var msg = _messages.Find(id) ?? throw new KeyNotFoundException($"Message(id:{id}) not found.");
            _messages.Remove(msg);
            _db.SaveChanges();
        }

        public void RemoveMessage(Message message)
        {
            _messages.Remove(message);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
