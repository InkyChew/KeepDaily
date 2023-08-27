using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategory();
        public Category? GetCategory(int id);
        public Category CreateCategory(Category ctg);
        public Category UpdateCategory(Category ctg);
        public void DeleteCategory(int id);
    }
}
