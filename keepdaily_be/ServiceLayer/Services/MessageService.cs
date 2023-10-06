using DomainLayer.Models;
using Microsoft.AspNetCore.SignalR;
using RepoLayer.IRepos;
using ServiceLayer.IServices;
using ServiceLayer.Utils;

namespace ServiceLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepo _repo;
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageService(IMessageRepo repo,
            IHubContext<MessageHub> hubContext)
        {
            _repo = repo;
            _hubContext = hubContext;
        }

        public List<Message> GetAllUserMessage(int uid)
        {
            return _repo.GetAllUserMessage(uid).ToList();
        }

        public async Task<Message> CreateMessage(int receiverId, Message message)
        {
            int UserMaxMsg = 20;
            var msgs = GetAllUserMessage(receiverId).ToList();
            if (msgs.Count == UserMaxMsg)
            {
                _repo.RemoveMessage(msgs.Last());
            }
            _repo.InsertMessage(message);
            _repo.SaveChanges();

            // sent event to user
            await _hubContext.Clients.Group($"{receiverId}").SendAsync("onMessageReceived", message);
            return message;
        }

        public void DeleteMessage(int id)
        {
            _repo.DeleteMessage(id);
        }
    }
}
