using CommonTests.RollbackStrategies;

namespace CommonTests.Commands
{
    class Command2 : SimpleTestCommand
    {
        public Command2(string groupName, bool emulateFail = false) 
            : base(null, emulateFail)
        {
            commandName = string.Format("{0}_command2", groupName);
        }
    }
}
