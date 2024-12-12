
using Repositories.IRepository;
using Repositories.UnitOfWork;

namespace VibeZOData.Services.Email
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IEmailSender emailSender, IUnitOfWork unitOfWork)
        {
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }
        public async Task SendNotificationAsync(Guid userId, string subject, string trackTitle)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            string content = $"Hello {user.Name},\n\nThe artist you follow has released a new track: \"{trackTitle}\". Check it out on VibeZ!";
            string emailSubject = subject;
            await _emailSender.SendNoticeEmail(user.Email, emailSubject, content);
        }
    }
}