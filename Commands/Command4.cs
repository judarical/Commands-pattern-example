using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Commands
{
    class Command4 : BaseCommand
    {
        public Command4(MultiCommand rollbackMultiCommand = null) : base(rollbackMultiCommand)
        {
        }

        protected override bool ExecuteAction()
        {
            Output("command 4 executed");
            return true;
        }

        public override void Rollback()
        {
            Output("command 4 canceled");
        }
    }
}
