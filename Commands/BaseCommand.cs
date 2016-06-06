﻿using System.IO;

namespace CommonTests.Commands
{
    abstract class BaseCommand : ICommand
    {
        private ICommand rollbackMultiCommand;

        #region constructors
        protected BaseCommand(MultiCommand rollbackMultiCommand = null)
        {
            SetRollbackMultiCommand(rollbackMultiCommand);
        }
        #endregion

        protected abstract bool ExecuteAction();

        public abstract void Rollback();

        public bool Execute()
        {
            if (!ExecuteAction())
            {
                BatchRollback();
                return false;
            }

            return true;
        }
        
        public void BatchRollback()
        {
            Rollback();
            
            if (rollbackMultiCommand != null)
                rollbackMultiCommand.Rollback();
        }

        public void SetRollbackMultiCommand(MultiCommand rollbackMultiCommand)
        {
            this.rollbackMultiCommand = rollbackMultiCommand;
        }
    }
}
