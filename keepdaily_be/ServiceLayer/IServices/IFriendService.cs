
using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IFriendService
    {
        public List<User> GetUserFriends(int uid);
        public Friend? GetFriend(int uid, int fid);
        public void AddFriend(Friend friend);
        public void DeleteFriend(Friend friend);
        public Message CreateFriendMessage(Friend friend);
    }
}
