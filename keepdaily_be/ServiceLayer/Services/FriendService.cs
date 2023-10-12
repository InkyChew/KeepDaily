using DomainLayer.Models;
using RepoLayer.IRepos;
using ServiceLayer.IServices;

namespace ServiceLayer.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepo _repo;
        private readonly IUserRepo _userRepo;
        private readonly IPlanRepo _planRepo;

        public FriendService(IFriendRepo repo, IUserRepo userRepo, IPlanRepo planRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _planRepo = planRepo;
        }

        public Friend? GetFriend(int uid, int fid)
        {
            return _repo.GetFriend(uid, fid);
        }

        public void DeleteFriend(Friend friend)
        {
            _repo.DeleteFriend(friend);
        }

        public List<User> GetUserFriends(int uid)
        {
            List<User> users = new ();
            var friends = _repo.GetAllFriend(uid).ToList();
            foreach(var f in friends)
            {
                var u = _userRepo.GetUser(f.FriendId);
                if (u != null)
                {
                    var latestPlan = _planRepo.GetAllPlan().OrderByDescending(_ => _.UpdateTime)
                                            .Where(_ => _.UserId == u.Id).FirstOrDefault();
                    if(latestPlan != null)
                    {
                        u.Plans = new List<Plan> { latestPlan };
                    }
                    users.Add(u);
                }
            }
            return users;
        }

        public void AddFriend(Friend friend)
        {
            _repo.InsertFriend(friend);
        }

        public Message CreateFriendMessage(Friend friend)
        {
            var follower = _userRepo.GetUser(friend.UserId)!;
            var imgurl = (follower.ImgName != null && follower.ImgType != null)
                            ? $"User/Img?name={follower.ImgName}&type={follower.ImgType}" : null;
            return new Message()
            {
                UserId = friend.FriendId,
                Title = "New Friend 🤝",
                Content = $"{follower.Name} has followed you.",
                Link = $"/friend/{follower.Id}",
                Image = imgurl
            };
        }
    }
}
