using CommonTests.RollbackStrategies;

namespace CommonTests.Commands
{
    class Command1 : SimpleTestCommand
    {
        public Command1(string groupName, bool emulateFail = false) 
            : base(null, emulateFail)
        {
            commandName = string.Format("{0}_command1", groupName);
        }
    }
}
