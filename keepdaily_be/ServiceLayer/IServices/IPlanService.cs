using DataLayer.Dto;
using DataLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IPlanService
    {
        public List<VMPlan> GetAllPlan();
        public VMPlan? GetPlan(int id);
        public Plan CreatePlan(VMPlan vmPlan);
        public Plan UpdatePlan(VMPlan vmPlan);
        public void DeletePlan(int id);
    }
}
