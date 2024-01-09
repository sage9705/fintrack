using FinTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTrack.Services
{
    public class CategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.GetCategoriesAsync();
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.SaveCategoryAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            await _context.SaveCategoryAsync(category);
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.GetCategoryAsync(id);
            if (category != null)
            {
                await _context.DeleteCategoryAsync(category);
            }
        }
    }
}