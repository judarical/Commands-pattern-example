using CommonTests.RollbackStrategies;

namespace CommonTests.Commands
{
    abstract class BaseCommand : ICommand
    {
        private IRollbackStrategy rollbackStrategy;

        protected bool executed;
        
        #region constructors
        /// <summary>
        /// Constructor accepts rollbackStrategy to manage rollbacks.
        /// </summary>
        /// <param name="rollbackStrategy"></param>
        protected BaseCommand(IRollbackStrategy rollbackStrategy)
        {
            SetRollbackStrategy(rollbackStrategy);
        }
        #endregion

        protected abstract bool ExecuteAction();

        protected abstract bool RollbackAction();
        
        public void Rollback()
        {            
            if (RollbackAction())
                executed = false;
        }
        
        public bool Execute()
        {
            // we don't want to repeat executed actions...
            if (executed || ExecuteAction()) {
                executed = true;
                return true;
            }
            
            if (rollbackStrategy == null)
            {
                // by default, we will rollback command itself
                rollbackStrategy = new RollbackOneStrategy(this);                
            }

            rollbackStrategy.Rollback();
            return false;            
        }

        public void SetRollbackStrategy(IRollbackStrategy rollbackStrategy)
        {
            this.rollbackStrategy = rollbackStrategy;
        }
    }
}
