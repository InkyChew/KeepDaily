using DomainLayer.Models;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.IServices
{
    public interface IDayService
    {
        public Day UpdateDay(Day day);
        public Day DeleteDay(int id);
    }
}
