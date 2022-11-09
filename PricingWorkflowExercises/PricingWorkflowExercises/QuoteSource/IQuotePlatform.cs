using PricingWorkflowExercises.Utils;
using System;

namespace PricingWorkflowExercises.QuoteSource
{
    internal interface IQuotePlatform
    {
        event EventHandler<Quote> QuoteRequest;
        event EventHandler<QuoteExecutionPrice> ExecutionRequest;
        event EventHandler<Quote> StopRequest;
        event EventHandler StopAllRequests;
    }
}
