namespace CommonTests.Commands
{
    interface ICommand
    {
        bool Execute();

        void Rollback();
    }
}
