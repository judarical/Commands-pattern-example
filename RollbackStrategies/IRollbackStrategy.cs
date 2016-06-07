using CommonTests.Commands;

namespace CommonTests.RollbackStrategies
{
    interface IRollbackStrategy
    {
        void Rollback();
    }
}
