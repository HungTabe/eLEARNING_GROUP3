using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineLearning.Hubs;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Controllers
{
    public class ChatController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IHubContext<UserChatHub> _userChatHubContext;

        public ChatController(
            IMessageService messageService, 
            IUserService userService, 
            IUserRoleService userRoleService,
            IHubContext<UserChatHub> userChatHubContext)
        {
            _messageService = messageService;
            _userService = userService;
            _userRoleService = userRoleService;
            _userChatHubContext = userChatHubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = long.Parse(userIdString);
            var chatPartners = await _messageService.GetChatPartnersAsync(userId);

            return View(chatPartners);
        }

        [HttpGet]
        public async Task<IActionResult> Chat(long partnerId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = long.Parse(userIdString);
            var currentUser = await _userService.GetUserByIdAsync(userId);
            var partner = await _userService.GetUserByIdAsync(partnerId);

            if (currentUser == null || partner == null)
            {
                return NotFound();
            }

            // Đánh dấu tin nhắn đã đọc
            await _messageService.MarkMessagesAsReadAsync(partnerId, userId);
            
            // Thông báo cho người gửi rằng tin nhắn đã được đọc thông qua SignalR
            await _userChatHubContext.Clients.Group(partnerId.ToString())
                .SendAsync("MessagesRead", userId);

            // Lấy lịch sử tin nhắn
            var messages = await _messageService.GetMessagesBetweenUsersAsync(userId, partnerId);

            ViewBag.CurrentUser = currentUser;
            ViewBag.Partner = partner;

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(long receiverId, string content)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var senderId = long.Parse(userIdString);
            
            if (string.IsNullOrWhiteSpace(content))
            {
                return RedirectToAction("Chat", new { partnerId = receiverId });
            }

            var message = await _messageService.SendMessageAsync(senderId, receiverId, content);
            
            // Gửi tin nhắn qua SignalR
            await _userChatHubContext.Clients.Group(receiverId.ToString())
                .SendAsync("ReceiveMessage", message);
            
            return RedirectToAction("Chat", new { partnerId = receiverId });
        }

        [HttpGet]
        public async Task<IActionResult> ChatWithMentor(long mentorId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = long.Parse(userIdString);
            
            // Kiểm tra xem người dùng có phải là mentee không
            var userRoles = await _userRoleService.GetRolesByUserIdAsync(userId);
            bool isMentee = userRoles.Contains(Enums.RoleType.MENTEE);
            
            if (!isMentee)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Chat", new { partnerId = mentorId });
        }
    }
}
