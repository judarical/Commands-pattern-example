using CommonTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Commands
{
    class FailCommand : BaseCommand
    {
        public FailCommand(MultiCommand rollbackMultiCommand = null) : base(rollbackMultiCommand)
        {
        }

        protected override bool ExecuteAction()
        {
            Logging.Output("failed command executed");
            return false;
        }

        public override void Rollback()
        {
            Logging.Output("failed command cancelled");
        }
    }
}
