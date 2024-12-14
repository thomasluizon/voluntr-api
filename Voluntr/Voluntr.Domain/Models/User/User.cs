using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class User : Entity
    {
        public Guid? OAuthProviderId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool Paused { get; set; }
        public bool EmailVerified { get; set; }
        public string Picture { get; set; }

        public ICollection<Notification> Notifications { get; set; } = [];

        public virtual ICollection<UserAddress> UserAddresses { get; set; } = [];
        public virtual ICollection<Address> Addresses
        {
            get => UserAddresses.Select(ua => ua.Address).ToList();
            set
            {
                UserAddresses.Clear();
                foreach (var address in value)
                {
                    UserAddresses.Add(new UserAddress
                    {
                        User = this,
                        Address = address
                    });
                }
            }
        }

        public virtual OAuthProvider OAuthProvider { get; set; }
    }
}
