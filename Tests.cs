using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CommonTests.Commands;
using CommonTests.Utils;
using CommonTests.RollbackStrategies;

namespace CommonTests
{
    enum Operators { OR, AND }

    [TestClass]
    public class Tests
    {
        private List<ICommand> BuildTestListOfCommands(string groupName)
        {
            return new List<ICommand> {
                new Command1(groupName),
                new Command2(groupName),
                new Command3(groupName),
                new Command4(groupName),
                new Command5(groupName)
            };
        }

        // it makes some sense in demonstration purposes...
        private MultiCommand BuildCommands()
        {
            #region Here we are creating all commands
            
            var fileGroup = new MultiCommand(BuildTestListOfCommands("file"));
            var dataSourceGroup = new MultiCommand(BuildTestListOfCommands("dataSource"));
            var otherTypeGroup = new MultiCommand(BuildTestListOfCommands("otherType"));
            var serviceCommandGroup = new MultiCommand(BuildTestListOfCommands("service"));
            
            var multiCommand = new MultiCommand (new List<ICommand>()
            {
                fileGroup,
                dataSourceGroup,
                otherTypeGroup,
                serviceCommandGroup
            });
            #endregion

            #region here we'll set strategies to commands
            // FULLROLLBACK
            var fullRollbackStrategy = new RollbackOneStrategy(multiCommand);
            // ONECOMMMANDROLLBACK 
            // this one will be executed by default if no strategy is selected

            // Certain GROUPROLLBACK (ONLY COMMANDS OF A SPECIFIC GROUP)
            var fileGroupRollBackStrategy = new RollbackOneStrategy(fileGroup);
            var serviceGroupRollBackStrategy = new RollbackOneStrategy(serviceCommandGroup);
            var dataGroupRollBackStrategy = new RollbackOneStrategy(dataSourceGroup);
            var otherGroupRollBackStrategy = new RollbackOneStrategy(otherTypeGroup);

            // COMBINATIONSOFCOMMANDSINDIFFERENTGROUPSROLLBACK | MORETHANONECOMMANDROLLBACK 
            var customRollback1 = new RollbackManyStrategy(new List<ICommand>
            {
                // rollback 2 file commands
                fileGroup.CommandAt(0),
                otherTypeGroup.CommandAt(2)                
            });

            var customRollback2 = new RollbackManyStrategy(new List<ICommand>
            {
                fileGroup.CommandAt(1),
                fileGroup.CommandAt(2),
                // rollback 2 dataSource commands
                dataSourceGroup.CommandAt(1),
                dataSourceGroup.CommandAt(4),
                // and we can also rollback the whole group of Service commands (if they are executed)
                otherTypeGroup
            });

            // NONEROLLBACKSTRATEGY
            // and this one can be used if we do not need to rollback a certain command if it ever fails
            var noneRollbackCommand = new RollbackNoneStrategy();
            #endregion

            #region Here we'll set up strategies for certain commands
            (serviceCommandGroup.CommandAt(3) as BaseCommand).SetRollbackStrategy(fullRollbackStrategy);

            (dataSourceGroup.CommandAt(0) as BaseCommand).SetRollbackStrategy(fileGroupRollBackStrategy);
            (serviceCommandGroup.CommandAt(4) as BaseCommand).SetRollbackStrategy(serviceGroupRollBackStrategy);
            (dataSourceGroup.CommandAt(3) as BaseCommand).SetRollbackStrategy(dataGroupRollBackStrategy);
            (otherTypeGroup.CommandAt(4) as BaseCommand).SetRollbackStrategy(otherGroupRollBackStrategy);

            (serviceCommandGroup.CommandAt(2) as BaseCommand).SetRollbackStrategy(customRollback1);
            (serviceCommandGroup.CommandAt(0) as BaseCommand).SetRollbackStrategy(customRollback2);

            #endregion

            return multiCommand;
        }

        [TestMethod]
        // FULLROLLBACK example
        public void TestScenario1()
        {
            Logging.Output("\r\nTestScenario1: FULLROLLBACK");
            var command = BuildCommands();
            // this will match serviceCommand4
            ((SimpleTestCommand)((MultiCommand)command.CommandAt(3)) // this will match serviceCommandGroup 
                .CommandAt(3)).SetEmulateFail(true); // this will match Command4
            command.Execute();
            Logging.Output(string.Empty);
        }

        [TestMethod]
        // CERTAINGROUP rollback example
        public void TestScenario2()
        {
            Logging.Output("\r\nTestScenario2: CERTAINGROUP");
            var command = BuildCommands();
            ((SimpleTestCommand)((MultiCommand)command.CommandAt(1)) // this will match dataSourceGroup 
                .CommandAt(0)) // this will match Command1
                .SetEmulateFail(true);
            command.Execute();
            Logging.Output(string.Empty);
            
        }

        [TestMethod]
        // THE SAME GROUP rollback example
        public void TestScenario3()
        {
            Logging.Output("\r\nTestScenario3: THE SAME GROUP");
            var command = BuildCommands();
            ((SimpleTestCommand)((MultiCommand)command.CommandAt(3)) // this will match serviceGroup 
                .CommandAt(4)) // this will match Command5
                .SetEmulateFail(true);
            command.Execute();
            Logging.Output(string.Empty);

        }

        [TestMethod]
        // COMBINATIONSOFCOMMANDSINDIFFERENTGROUPSROLLBACK | MORETHANONECOMMANDROLLBACK 
        public void TestScenario4()
        {
            Logging.Output("\r\nTestScenario4: MORETHANONECOMMANDROLLBACK");
            var command = BuildCommands();
            ((SimpleTestCommand)((MultiCommand)command.CommandAt(3)) // this will match serviceGroup 
                .CommandAt(2)) // this will match Command3
                .SetEmulateFail(true);
            command.Execute();
            Logging.Output(string.Empty);

        }
        
        [TestMethod]
        // 2: COMBINATIONSOFCOMMANDSINDIFFERENTGROUPSROLLBACK | MORETHANONECOMMANDROLLBACK 
        public void TestScenario5()
        {
            Logging.Output("\r\nTestScenario5: MORETHANONECOMMANDROLLBACK 2");
            var command = BuildCommands();
            ((SimpleTestCommand)((MultiCommand)command.CommandAt(3)) // this will match serviceGroup 
                .CommandAt(0)) // this will match Command1
                .SetEmulateFail(true);
            command.Execute();
            Logging.Output(string.Empty);
        }

        [TestMethod]
        // ONECOMMMANDROLLBACK 
        // this doesn't rollback anything, because nothing was executed... the theme for discussion
        public void TestScenario6()
        {
            Logging.Output("\r\nTestScenario6: ONECOMMMANDROLLBACK");
            var command = BuildCommands();
            ((SimpleTestCommand)((MultiCommand)command.CommandAt(0)) // this will match fileGroup 
                .CommandAt(2)) // this will match Command3
                .SetEmulateFail(true);
            command.Execute();
            Logging.Output(string.Empty);

        }
    }
}

