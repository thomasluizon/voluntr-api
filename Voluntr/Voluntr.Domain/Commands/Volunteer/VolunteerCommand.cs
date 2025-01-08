using Voluntr.Crosscutting.Domain.Commands;

namespace Voluntr.Domain.Commands
{
    public abstract class VolunteerCommand : Command
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public AddressCommand Address { get; set; }
    }
}
