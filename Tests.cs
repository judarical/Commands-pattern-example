using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CommonTests.Commands;

namespace CommonTests
{
    enum Operators { OR, AND }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestScenario1()
        {
            var command1 = new Command1();
            var command2 = new Command2();
            var command3 = new Command3(new MultiCommand(null, new List<ICommand> { command1 }));
            var command4 = new FailCommand(new MultiCommand(null, new List<ICommand> { command1, command2, command3 }));
            
            var group1 = new MultiCommand(new List<ICommand>
            {
               command1,
               command2
            }, null);

            var group2 = new MultiCommand(new List<ICommand>
            {
               command3,
               command4
            }, null);

            var groups = new MultiCommand(new List<ICommand>
            {
                group1,
                group2
            }, null);

            groups.Execute();
        }

        [TestMethod]
        public void TestScenario2()
        {
            var command1 = new Command1();
            var command2 = new Command2();
            var command3 = new FailCommand(new MultiCommand(null, new List<ICommand> { command1 }));
            var command4 = new Command4(new MultiCommand(null, new List<ICommand> { command1, command2, command3 }));
            
            var group1 = new MultiCommand(new List<ICommand>
            {
               command1,
               command2
            }, null);

            var group2 = new MultiCommand(new List<ICommand>
            {
               command3,
               command4
            }, null);

            var groups = new MultiCommand(new List<ICommand>
            {
                group1,
                group2
            }, null);

            groups.Execute();
        }
    }
}
