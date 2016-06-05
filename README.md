# Commands-pattern-example
This is axample for Stackoverflow Topic http://stackoverflow.com/a/37631844/2357405

The question is:

5
down vote
favorite
3
I've created a rollback/retry mechanism using the Command Pattern and a custom retry mechanism with the RetryCount and timeout threshold properties as input (As described here Which is the best way to add a retry/rollback mechanism for sync/async tasks in C#?). I also tried the Polly framework instead and it is great!

Now, I want to wrap them in an abstraction. I could describe it as commands plan mechanism or command based decision mechanism.

So, Based on the different combinations of command results I have to decide which of the accomplished commands will be reverted and which of them not (I want to offer the ability to press a ReTry button and some off the commands shouldn't be reverted).

These commands are grouped in some categories and I have to take different rollback strategy for the different groups and commands (based on the result). Some kind of different policies would take place here!

IMPORTANT: I want to avoid IF/ELSE etc.Maybe the Chain-of-responsibility pattern would help me but I DON'T know and I really want help:

//Pseudo-code...

CommandHandler actionManager1 = new Action1Manager();
CommandHandler actionManager2 = new Action2Manager();
CommandHandler actionManager2 = new Action3Manager();

actionManager1.SetNextObjManager(Action2Manager);
actionManager2.SetNextObjManager(Action3Manager);

actionManager1.ProcessAction(){...}
actionManager1.ProcessAction(){...}
...
Another idea could be observables or event handlers but the question is how to use them (?). Or just by using a list/stack/queue of Commands and check that list to take the next-step decision (?):

  private List<ICommand> rollbackCommandsOfAllGroups;
  private List<ICommand> rollbackCommandsOfGroup1;
  private List<ICommand> rollbackCommandsOfGroup2;
...
SCENARIOS

Finally, You can think of twenty (20) commands grouped in four (4) categories.

Scenario 1

Group 1 Commands (5) were successful
Group 2 Commands 1 and 2 were successful but Command 3 was FAILED.Command 4 and 5 not executed
Group 3 not executed
Group 4 not executed
Decision 1

Rollback Commands 1 and 2 from Group 2 BUT not the whole Group 1 Commands
Scenario 2

Group 1 Commands (5) were successful
Group 2 Commands (5) were successful
Group 3 Commands (5) were successful
Group 4 Commands 1 - 4 were successful BUT Command 5 was FAILED
Decision 2

Rollback All Commands from all Groups
I want to guide me and help me with CODE examples in C#.
