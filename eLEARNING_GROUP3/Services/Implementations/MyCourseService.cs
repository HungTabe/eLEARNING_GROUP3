using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class MyCourseService : IMyCourseService
    {
        private readonly IMyCourseRepository _myCourseRepo;
        private readonly IWishlistRepository _wishListRepo;

        public MyCourseService(IMyCourseRepository myCourseRepo, IWishlistRepository wishListRepo) {
            _myCourseRepo = myCourseRepo;
            _wishListRepo = wishListRepo;
        }

        public async Task<ResultDto> AddToWishlistAsync(long userId, long courseId)
        {
            // Kiểm tra xem đã có trong wishlist chưa
            var existingWishList = await _wishListRepo.ExistingWishListAsync( userId,  courseId);
            if (existingWishList != null)
            {
                return new ResultDto
                {
                    Success = false,
                    Message = "This course is already in your wishlist."
                };
            }
            var wishList = new WishList
            {
                CourseId = courseId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

           await _wishListRepo.AddAsync(wishList);
            return new ResultDto
            {
                Success = true,
                Message = "Course successfully added to wishlist."
            };
        }

        public async Task<List<MyCourseDto>> GetMyCourseList(long userId)
        {

            var result = await _myCourseRepo.GetCourseAndProgressById(userId);
            return result != null ? result : new List<MyCourseDto>();
        }

        public async Task<List<WishlistDTO>> GetMyWishlist(long userId)
        {
            var result = await _myCourseRepo.GetMyWishListByUserId(userId);
            return result != null ? result : new List<WishlistDTO>();
        }
    }
}
