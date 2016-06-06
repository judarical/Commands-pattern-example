using CommonTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Commands
{
    class Command3 : BaseCommand
    {
        public Command3(MultiCommand rollbackMultiCommand = null) : base(rollbackMultiCommand)
        {
        }

        protected override bool ExecuteAction()
        {
            Logging.Output("command 3 executed");
            return true;
        }
        
        public override void Rollback()
        {
            Logging.Output("command 3 canceled");
        }        
    }
}
