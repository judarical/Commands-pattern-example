using System;

namespace CommonTests.RollbackStrategies
{
    class RollbackNoneStrategy : IRollbackStrategy
    {
        public void Rollback()
        {
            // nothing to do here... 
        }
    }
}
