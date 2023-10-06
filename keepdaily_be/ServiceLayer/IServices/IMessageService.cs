using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IMessageService
    {
        public List<Message> GetAllUserMessage(int uid);
        public Task<Message> CreateMessage(int receiverId, Message message);
        public void DeleteMessage(int id);
    }
}
