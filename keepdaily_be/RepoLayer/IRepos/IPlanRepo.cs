using DomainLayer.Models;

namespace RepoLayer.IRepos
{
    public interface IPlanRepo
    {
        public IEnumerable<Plan> GetAllPlan();
        public IEnumerable<Plan> GetAllPlanWithDetail();
        public Plan? GetPlan(int id);
        public Plan? GetPlanWithDetail(int id);
        public Plan InsertPlan(Plan plan);
        public Plan UpdatePlan(Plan plan);
        public void DeletePlan(int id);
        public Plan FindPlan(int id);
    }
}
