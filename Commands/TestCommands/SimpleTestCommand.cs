using CommonTests.RollbackStrategies;
using CommonTests.Utils;

namespace CommonTests.Commands
{
    abstract class SimpleTestCommand : BaseCommand
    {
        protected string commandName;

        protected bool emulateFail;

        public SimpleTestCommand(IRollbackStrategy rollbackStrategy = null, bool emulateFail = false) 
            : base(rollbackStrategy)
        {
            SetEmulateFail(emulateFail);
        }

        protected sealed override bool ExecuteAction()
        {
            if (emulateFail)
            {
                Logging.Output(string.Format("'{0}' failed", commandName));
                return false;
            }
            else
            {
                Logging.Output(string.Format("'{0}' executed", commandName));
                return true;
            }
        }

        protected sealed override bool RollbackAction()
        {
            if (!executed) return false;
            Logging.Output(string.Format("'{0}' canceled", commandName));
            return true;
        }

        public void SetEmulateFail(bool emulateFail)
        {
            this.emulateFail = emulateFail;
        }
    }
}
