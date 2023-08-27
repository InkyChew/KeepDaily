using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;

namespace RepoLayer.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<Category> _categories;

        public CategoryRepo(KeepDailyContext db)
        {
            _db = db;
            _categories = db.Category;
        }

        public void DeleteCategory(int id)
        {
            var ctg = _categories.Find(id);
            if (ctg == null) throw new KeyNotFoundException($"Can not find Category(Id:{id})");
            _categories.Remove(ctg);
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _categories;
        }

        public Category? GetCategory(int id)
        {
            return _categories.SingleOrDefault(_ => _.Id == id);
        }

        public Category InsertCategory(Category ctg)
        {
            _categories.Add(ctg);
            _db.SaveChanges();
            return ctg;
        }

        public Category UpdateCategory(Category ctg)
        {
            _categories.Update(ctg);
            _db.SaveChanges();
            return ctg;
        }
    }
}
