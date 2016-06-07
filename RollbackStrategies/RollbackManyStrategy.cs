using CommonTests.Commands;
using System.Collections.Generic;

namespace CommonTests.RollbackStrategies
{
    class RollbackManyStrategy : IRollbackStrategy
    {
        List<ICommand> rollbackCommands;

        /// <summary>
        /// The list of commands in the order they should rollback.
        /// </summary>
        /// <param name="rollbackCommands"></param>
        public RollbackManyStrategy(List<ICommand> rollbackCommands)
        {
            this.rollbackCommands = rollbackCommands;
        }

        public void Rollback()
        {
            foreach (var rollbackCommand in rollbackCommands)
            {
                rollbackCommand.Rollback();
            }
        }
    }
}
