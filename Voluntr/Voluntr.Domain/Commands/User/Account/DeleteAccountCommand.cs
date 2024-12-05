namespace Voluntr.Domain.Commands
{
    public class DeleteAccountCommand : UserCommand
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
