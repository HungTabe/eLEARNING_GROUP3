using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(OnlLearnDBContext context) : base(context)
        {
        }



        public async Task<IEnumerable<Module>> GetAllModuleByCourseIdAysnc(long courseId)
        {
            return await _context.Modules
                        .Where(c => c.CourseId == courseId)
                        .OrderBy(c => c.ModuleNumber)
                        .ToListAsync();
        }

        public async Task<Module> GetModuleAsync(long courseId, int moduleNumber)
        {
            return await _context.Modules.SingleOrDefaultAsync(m => m.CourseId == courseId && m.ModuleNumber == moduleNumber);

        }

        public async Task<int> GetNextModuleNumberAsync(long courseId)
        {
            var existingModules = await _context.Modules
            .Where(m => m.CourseId == courseId)
            .OrderBy(m => m.ModuleNumber)
            .ToListAsync();

            int nextModuleNumber = 1; // Mặc định nếu chưa có module nào

            if (existingModules.Any())
            {
                // Lấy danh sách số thứ tự module đã tồn tại
                var moduleNumbers = existingModules.Select(m => m.ModuleNumber).ToList();

                // Tìm số nhỏ nhất chưa có trong danh sách
                for (int i = 1; i <= moduleNumbers.Count + 1; i++)
                {
                    if (!moduleNumbers.Contains(i))
                    {
                        nextModuleNumber = i;
                        break;
                    }
                }
            }
            return nextModuleNumber;
        }


    }

}
