namespace Voluntr.Domain.Commands
{
    public class DeletePictureCommand : UserCommand
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
