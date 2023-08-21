using DataLayer.Models;

namespace RepoLayer.IRepos
{
    public interface IPlanRepo
    {
        public IEnumerable<Plan> GetAllPlan();
        public Plan? GetPlan(int id);
        public Plan CreatePlan(Plan plan);
        public Plan UpdatePlan(Plan plan);
        public void DeletePlan(int id);
        public Plan FindPlan(int id);
    }
}
