using DomainLayer.Models;
using RepoLayer.IRepos;
using ServiceLayer.IServices;

namespace ServiceLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;

        public CategoryService(ICategoryRepo repo)
        {
            _repo = repo;
        }

        public void DeleteCategory(int id)
        {
            _repo.DeleteCategory(id);
        }

        public List<Category> GetAllCategory()
        {
            return _repo.GetAllCategory().OrderBy(_ => _.Name).ToList();
        }

        public Category? GetCategory(int id)
        {
            return _repo.GetCategory(id);
        }

        public Category CreateCategory(Category ctg)
        {
            return _repo.InsertCategory(ctg);
        }

        public Category UpdateCategory(Category ctg)
        {
            return _repo.UpdateCategory(ctg);
        }
    }
}
