namespace Voluntr.Domain.Commands
{
    public class ToggleUserPauseCommand : UserCommand
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
