using DomainLayer.Models;

namespace RepoLayer.IRepos
{
    public interface IFriendRepo
    {
        public IEnumerable<Friend> GetAllFriend(int uid);
        public Friend? GetFriend(int uid, int fid);
        public void InsertFriend(Friend friend);
        public void DeleteFriend(Friend friend);
    }
}
