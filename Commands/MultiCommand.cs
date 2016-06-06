using CommonTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Commands
{
    class MultiCommand : ICommand
    {
        private List<ICommand> rollbackCommands;

        private List<ICommand> commands;

        public MultiCommand(List<ICommand> commands, List<ICommand> rollbackCommands)
        {
            this.rollbackCommands = rollbackCommands;
            if (rollbackCommands != null)
                this.rollbackCommands.Reverse();
            this.commands = commands;
        }

        #region not implemented members
        public void BatchRollback()
        {
            foreach (var rollbackCommand in rollbackCommands)
            {
                rollbackCommand.Rollback();
            }
        }

        /// <summary>
        /// This method will execute all the commands one by one.
        /// </summary>
        /// <returns>Returns true if all commands successfully executed.</returns>
        public bool Execute()
        {
            foreach (var command in commands)
            {
                if (!command.Execute())
                   return false;               
            }

            return true;
        }

        /// <summary>
        /// This is the way to cancel all commands in the group.
        /// </summary>
        public void Rollback()
        {
            var reversedCommands = rollbackCommands.FastReverse();
            foreach (var command in reversedCommands)
            {
                command.Rollback();
            }
        }
        #endregion
    }
}
