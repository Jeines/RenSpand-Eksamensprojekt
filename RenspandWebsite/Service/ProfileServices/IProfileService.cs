using RenspandWebsite.Models;

namespace RenspandWebsite.Service.ProfileServices
{
    public interface IProfileService
    {
        List<Profile> Profiles { get; set; }

        bool ValidatePassword(int id, string inputPassword);
        Profile GetUserData(int id);
        void UpdateUserData(int id, Profile profile);
        void AddProfile(Profile profile);
        void UpdatePassWord(int id, string newPassword);
        Task<List<Order>> GetUserOrders(int userId);
        void RemoveProfile(int id);
    }
}
