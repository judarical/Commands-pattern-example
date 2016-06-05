﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Commands
{
    class Command1 : BaseCommand
    {
        public Command1(MultiCommand rollbackMultiCommand = null) : base(rollbackMultiCommand)
        {
        }

        protected override bool ExecuteAction()
        {
            Output("command 1 executed");
            return true;
        }
        
        public override void Rollback()
        {
            Output("command 1 canceled");
        }
    }
}
