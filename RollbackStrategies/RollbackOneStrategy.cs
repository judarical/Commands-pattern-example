using CommonTests.Commands;

namespace CommonTests.RollbackStrategies
{
    class RollbackOneStrategy : IRollbackStrategy
    {
        private readonly ICommand commandToRollback;

        public RollbackOneStrategy(ICommand commandToRollback)
        {
            this.commandToRollback = commandToRollback;
        }

        public void Rollback()
        {
            commandToRollback.Rollback();
        }
    }
}
