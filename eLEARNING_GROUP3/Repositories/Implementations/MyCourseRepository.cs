using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class MyCourseRepository : BaseRepository<Course>, IMyCourseRepository
    {
        public MyCourseRepository(OnlLearnDBContext context) : base(context) { }

        public async Task<List<MyCourseDto>> GetCourseAndProgressById(long id)
        {
            var result = from c in _context.Courses
                         join e in _context.CourseEnrollments on c.CourseId equals e.CourseId
                         join cImage in _context.CourseImageUrls on c.CourseId equals cImage.CourseId into imageGroup
                         where e.UserId == id
                         select new MyCourseDto
                         {
                            CourseId = c.CourseId,
                             CourseName = c.CourseName,
                             Price = c.Price,
                             Creator = c.Creator,
                             CourseUrls = imageGroup.Select(img => img.Url).ToList(),
                             Description = c.Description,
                             CreatedAt = e.CreatedAt,
                             Progress =  e.Progress
                         };


            return await result.ToListAsync() ?? new List<MyCourseDto>();
        }

        public async Task<List<WishlistDTO>> GetMyWishListByUserId(long userId)
        {
            try
            {
                var result = from w in _context.WishLists
                             join c in _context.Courses on w.CourseId equals c.CourseId
                             join cImage in _context.CourseImageUrls on c.CourseId equals cImage.CourseId into imageGroup
                             join u in _context.Users on w.UserId equals u.UserId
                             where w.UserId == userId
                             select new WishlistDTO
                             {
                                 CourseId = c.CourseId,
                                 CourseName = c.CourseName,
                                 Price = c.Price,
                                 Creator = c.Creator,
                                 CourseUrls = imageGroup.Select(img => img.Url).ToList(),
                                 Description = c.Description,
                             };


                return await result.ToListAsync() ?? new List<WishlistDTO>();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật dữ liệu, vui lòng thử lại.");
            }
        }
    }
}
