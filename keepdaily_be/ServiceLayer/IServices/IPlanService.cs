using DomainLayer.Dto;
using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IPlanService
    {
        public IEnumerable<Plan> GetAllPlan();
        public List<VMPlan> GetAllPlanWithDetail();
        public Plan? GetPlan(int id);
        public Plan? GetPlanWithDetail(int id, int? year = null, int? month = null);
        public Plan CreatePlan(Plan plan);
        public Plan CreatePlanWithDetail(VMPlan vmPlan);
        public Plan UpdatePlan(Plan plan);
        public Plan UpdatePlanWithDetail(VMPlan vmPlan);
        public void DeletePlan(int id);
    }
}
