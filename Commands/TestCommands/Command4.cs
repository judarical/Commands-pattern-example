using CommonTests.RollbackStrategies;

namespace CommonTests.Commands
{
    class Command4 : SimpleTestCommand
    {
        public Command4(string groupName, bool emulateFail = false) 
            : base(null, emulateFail)
        {
            commandName = string.Format("{0}_command4", groupName);
        }
    }
}
