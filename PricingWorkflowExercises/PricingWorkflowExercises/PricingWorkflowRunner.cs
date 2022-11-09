using PricingWorkflowExercises.Pricer;
using PricingWorkflowExercises.QuoteSource;
using PricingWorkflowExercises.Utils;
using System;

namespace PricingWorkflowExercises
{
    internal class PricingWorkflowRunner : IPricingWorkflowRunner
    {
        public event EventHandler<QuotePrice> QuotePriceUpdated;
        public event EventHandler<QuoteExecutionPrice> QuoteExecutionSuccessful;
        public event EventHandler<QuoteExecutionPrice> QuoteExecutionFailed;
        public event EventHandler<Quote> QuoteStopped;
        public event EventHandler<Quote> QuoteStarted;
        public event EventHandler AllQuotesStopped;

        public PricingWorkflowRunner(IQuotePlatform quotePlatform, IPricer pricer)
        {
        }

        public void OnExecutionRequest(object sender, QuoteExecutionPrice executionPrice)
        {
            throw new NotImplementedException();
        }

        public void OnPriceUpdate(object sender, QuotePrice quotePrice)
        {
            throw new NotImplementedException();
        }

        public void OnQuoteRequest(object sender, Quote quote)
        {
            throw new NotImplementedException();
        }

        public void OnStopAllRequests(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public void OnStopRequest(object sender, Quote quote)
        {
            throw new NotImplementedException();
        }
    }
}