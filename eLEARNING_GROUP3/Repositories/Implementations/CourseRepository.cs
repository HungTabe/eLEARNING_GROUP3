using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using System.Linq;

namespace OnlineLearning.Repositories.Implementations
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(OnlLearnDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllCourseAsync()
        {
            return await _context.Courses
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.CourseImageUrls)
                .Include(c => c.CourseCategories).ThenInclude(cc => cc.Category) // incluce bảng category
                .ToListAsync();
        }


        public async Task<IEnumerable<Course>> GetAllCourseByStatusAsync()
        {
            return await _context.Courses
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.CourseImageUrls)
                .Include(c => c.CourseCategories).ThenInclude(cc => cc.Category)
                .Where(c => c.Status == Enums.CourseStatus.Approved)
                .ToListAsync();
        }



        public async Task<IEnumerable<Admin_ReviewCourseDto>> GetCoursesListNotApproveYetAsync()
        {
            return await _context.Courses
                        .Where(x => x.Status == Enums.CourseStatus.Pending)
                        .Include(x => x.CreatorUser)
                        .GroupJoin(_context.CourseImageUrls,
                        c => c.CourseId,
                        cImg => cImg.CourseId,
                        (c, imageGroup) => new { Course = c, Images = imageGroup })
                        .OrderByDescending(x => x.Course.CreatedAt)
                        .Select(x => new Admin_ReviewCourseDto
                        {
                            CourseId = x.Course.CourseId,
                            CourseName = x.Course.CourseName,
                            CreatedAt = x.Course.CreatedAt,
                            CreatorName = x.Course.CreatorUser.FullName,
                            Description = x.Course.Description,
                            CourseImgUrl = x.Images.Select(i => i.Url).FirstOrDefault()
                        })
                            .ToListAsync();
        }

        public async Task<Course> GetInforCourseByIdAsync(long courseId)
        {
            return await _context.Courses
                .Include(c => c.CreatorUser)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.CourseImageUrls)
                .Include(c => c.CourseCategories).ThenInclude(cc => cc.Category)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);

        }

        // GET AMOUNT RATING 
        public async Task<int> GetAmountRatingAsync(long courseId)
        {
            return await _context.CourseRatings.CountAsync(r => r.CourseId == courseId);
        }

        // GET LIST OF RATINGS
        public async Task<List<CourseRating>> GetRatingsAsync(long courseId)
        {
            return await _context.CourseRatings
                                 .Where(r => r.CourseId == courseId)
                                 .Include(r => r.User)
                                 .ToListAsync();
        }


        // GET RELATED COURSES
        public async Task<List<CoursePreviewDTO>> GetRelatedCoursesAsync(long courseId)
        {
            var currentCourse = await _context.Courses
                .Where(c => c.CourseId == courseId)
                .Select(c => new
                {
                    CategoryIds = c.CourseCategories.Select(cc => cc.CategoryId).ToList()
                })
                .FirstOrDefaultAsync();

            if (currentCourse == null)
                return new List<CoursePreviewDTO>();

            return await _context.Courses
                .Where(rc => rc.CourseCategories.Any(cat => currentCourse.CategoryIds.Contains(cat.CategoryId)) && rc.CourseId != courseId)
                .OrderByDescending(rc => rc.CreatedAt)
                .Take(5)
                .Select(rc => new CoursePreviewDTO
                {
                    CourseId = rc.CourseId,
                    CourseName = rc.CourseName,
                    Image = rc.CourseImageUrls.FirstOrDefault().Url,
                    Price = rc.Price,
                    AvgRating = _context.CourseRatings.Where(r => r.CourseId == rc.CourseId).Average(r => (decimal?)r.Rating) ?? 0,
                    SumOfRating = _context.CourseRatings.Count(r => r.CourseId == rc.CourseId),
                })
                .ToListAsync();
        }
        // GET NUMBER OR LECTURES 
        public async Task<int> GetQuantityLecture(long courseId)
        {
            return await _context.Lessons
                    .Where(l => l.Module.CourseId == 1)
                    .CountAsync();
        }
        // GET MODULE WITH LESSON (for mentee lesson)
        public async Task<List<Module>> GetModulesWithLessonsAsync(long courseId)
        {
            var modules = await _context.Modules
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Lessons)
                .OrderBy(m => m.ModuleNumber)
                .ToListAsync();

            // Sắp xếp danh sách bài học sau khi lấy từ database
            foreach (var module in modules)
            {
                module.Lessons = module.Lessons.OrderBy(l => l.LessonNumber).ToList();
            }

            return modules;
        }

        // GET DETAIL COURSE (USER)
        public async Task<CourseDetailDTO> GetCourseDetailAsync(long courseId)
        {
            var course = await GetInforCourseByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found.");
            var listRating = await GetRatingsAsync(courseId);
            var avgRating = listRating.Any() ? listRating.Average(r => r.Rating) : 0;
            var amountRating = await GetAmountRatingAsync(courseId);
            var relatedCourses = await GetRelatedCoursesAsync(courseId);
            var enrollmentCount = await _context.CourseEnrollments.CountAsync(e => e.CourseId == courseId);
            var lectures = await GetQuantityLecture(courseId);
            var modules = await GetModulesWithLessonsAsync(courseId);
            return new CourseDetailDTO
            {
                Course = course,
                AvgRating = avgRating,
                AmountRating = amountRating,
                EnrollmentCount = enrollmentCount,
                RelatedCourses = relatedCourses,
                courseRatings = listRating,
                Modules = modules
            };
        }

        public async Task<bool> CheckUserPurchaseCourseAsync(long? userId, long courseId)
        {
            if (!userId.HasValue) return false;
            var transactionHistory = await _context.TransactionHistories
                                    .FirstOrDefaultAsync(t => t.UserId == userId
                                                    && t.CourseId == courseId
                                                    && t.Status == Enums.TransactionStatus.Completed
                                                    && t.TransactionType.Equals(Enums.TransactionType.External));

            return transactionHistory != null;
        }
    }
}
