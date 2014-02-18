namespace Repository.Pattern.Command
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}