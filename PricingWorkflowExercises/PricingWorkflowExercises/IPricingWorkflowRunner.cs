using PricingWorkflowExercises.Utils;
using System;

namespace PricingWorkflowExercises
{
    internal interface IPricingWorkflowRunner
    {
        event EventHandler<QuotePrice> QuotePriceUpdated;
        event EventHandler<QuoteExecutionPrice> QuoteExecutionSuccessful;
        event EventHandler<QuoteExecutionPrice> QuoteExecutionFailed;
        event EventHandler<Quote> QuoteStopped;
        event EventHandler<Quote> QuoteStarted;
        event EventHandler AllQuotesStopped;
        void OnQuoteRequest(object sender, Quote quote);
        void OnPriceUpdate(object sender, QuotePrice quotePrice);
        void OnExecutionRequest(object sender, QuoteExecutionPrice executionPrice);
        void OnStopRequest(object sender, Quote quote);
        void OnStopAllRequests(object sender, EventArgs eventArgs);

    }
}