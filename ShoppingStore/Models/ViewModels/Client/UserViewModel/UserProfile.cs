namespace WebApi.Models.ViewModels.Client.UserViewModel
{
    public class UserProfile
    {
        public string UserName { get; set; }

        public string Phone { get; set; }
        public string? ProfilePicture { get; set; } = null!;
        public string ExpireTime { get; set; }    

        public bool HaveTime { get; set; }
    }
}
