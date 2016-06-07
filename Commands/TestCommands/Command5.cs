using CommonTests.RollbackStrategies;

namespace CommonTests.Commands
{
    class Command5 : SimpleTestCommand
    {
        public Command5(string groupName, bool emulateFail = false) 
            : base(null, emulateFail)
        {
            commandName = string.Format("{0}_command5", groupName);
        }
    }
}
