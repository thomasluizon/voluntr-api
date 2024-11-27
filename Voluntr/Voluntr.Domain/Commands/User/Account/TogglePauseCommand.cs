namespace Voluntr.Domain.Commands
{
    public class TogglePauseCommand : UserCommand
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
