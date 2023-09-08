using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;

namespace RepoLayer.Repos
{
    public class FriendRepo : IFriendRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<Friend> _friends;

        public FriendRepo(KeepDailyContext db)
        {
            _db = db;
            _friends = db.Friend;
        }

        public IEnumerable<Friend> GetAllFriend(int uid)
        {
            return _friends.Where(_ => _.UserId == uid);
        }

        public Friend? GetFriend(int uid, int fid)
        {
            return _friends.SingleOrDefault(_ => _.UserId == uid && _.FriendId == fid);
        }

        public void InsertFriend(Friend friend)
        {
            _friends.Add(friend);
            _db.SaveChanges();
        }

        public void DeleteFriend(Friend friend)
        {
            _friends.Remove(friend);
            _db.SaveChanges();
        }
    }
}
