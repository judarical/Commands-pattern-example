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
            throw new NotImplementedException();
        }

        public bool Execute()
        {
            foreach (var command in commands)
            {
                if (!command.Execute())
                    return false;               
            }

            return true;
        }

        public void Rollback()
        {
            foreach (var rollbackCommand in rollbackCommands)
            {
                rollbackCommand.Rollback();
            }
        }
        #endregion
    }
}
