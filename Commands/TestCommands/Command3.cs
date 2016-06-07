using CommonTests.RollbackStrategies;

namespace CommonTests.Commands
{
    class Command3 : SimpleTestCommand
    {
        public Command3(string groupName, bool emulateFail = false) 
            : base(null, emulateFail)
        {
            commandName = string.Format("{0}_command3", groupName);
        }
    }
}
