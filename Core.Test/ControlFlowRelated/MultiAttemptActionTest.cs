using System;
using Core.Extensions.ControlFlowRelated;
using Xunit;

namespace Core.Test.ControlFlowRelated;

public class MultiAttemptActionTest
{
    [Fact]
    public void MultiAttemptTest()
    {
        var attemptCount = 0;
        Action action =  () =>
        {
            attemptCount++;
            if (attemptCount <= 3)
                throw new InvalidOperationException();
        };
        var multiAction = action.WithMultipleAttempts();
        multiAction.Invoke();
        Assert.Equal(4, multiAction.AttemptCount);
    }
}