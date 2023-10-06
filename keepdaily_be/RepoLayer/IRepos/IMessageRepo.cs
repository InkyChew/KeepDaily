using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.IRepos
{
    public interface IMessageRepo
    {
        public IEnumerable<Message> GetAllMessage();
        public IEnumerable<Message> GetAllUserMessage(int uid);
        public void InsertMessage(Message message);
        public void DeleteMessage(int id);
        public void RemoveMessage(Message message);
        public void SaveChanges();
    }
}
