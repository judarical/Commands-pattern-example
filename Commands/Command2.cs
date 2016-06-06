﻿using CommonTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Commands
{
    class Command2 : BaseCommand
    {
        public Command2(MultiCommand rollbackMultiCommand = null) : base(rollbackMultiCommand)
        {
        }

        protected override bool ExecuteAction()
        {
            Logging.Output("command 2 executed");
            return true;
        }

        public override void Rollback()
        {
            Logging.Output("command 2 canceled");
        }
    }
}
