using DomainLayer.Models;

namespace RepoLayer.IRepos
{
    public interface ICategoryRepo
    {
        public IEnumerable<Category> GetAllCategory();
        public Category? GetCategory(int id);
        public Category InsertCategory(Category ctg);
        public Category UpdateCategory(Category ctg);
        public void DeleteCategory(int id);
    }
}
