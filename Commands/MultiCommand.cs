using CommonTests.RollbackStrategies;
using CommonTests.Utils;
using System.Collections.Generic;

namespace CommonTests.Commands
{
    class MultiCommand : BaseCommand
    {
        private List<ICommand> commands;

        private IRollbackStrategy rollbackStrategy;

        public MultiCommand(List<ICommand> commands = null)
            : base (new RollbackNoneStrategy())
        {
            SetCommands(commands, rollbackStrategy);
        }        
                
        /// <summary>
        /// This is the way to cancel all commands in the group.
        /// </summary>
        protected override bool RollbackAction()
        {
            foreach (var command in commands.FastReverse())
            {
                command.Rollback();
            }

            return true;
        }

        protected override bool ExecuteAction()
        {
            foreach (var command in commands)
            {
                if (!command.Execute())
                    return false;     
            }

            return true;
        }

        public ICommand CommandAt(int index)
        {
            return commands[index];
        }

        public void SetCommands(List<ICommand> commands, IRollbackStrategy rollbackStrategy)
        {
            this.rollbackStrategy = rollbackStrategy;
            this.commands = commands;
        }
    }
}
