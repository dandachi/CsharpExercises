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
        private IPricer _pricer;

        public PricingWorkflowRunner(IQuotePlatform quotePlatform, IPricer pricer)
        {
            quotePlatform.QuoteRequest += OnQuoteRequest;
            quotePlatform.ExecutionRequest += OnExecutionRequest;
            quotePlatform.StopRequest += OnStopRequest;
            quotePlatform.StopAllRequests += OnStopAllRequests;
            pricer.PriceUpdated += OnPriceUpdate;

            _pricer = pricer;
        }

        public void OnExecutionRequest(object sender, QuoteExecutionPrice executionPrice)
        {
            // add more code
        }

        public void OnPriceUpdate(object sender, QuotePrice quotePrice)
        {
            // handle new price from spot pricer
            QuotePriceUpdated?.Invoke(this, quotePrice);
        }

        public void OnQuoteRequest(object sender, Quote quote)
        {
            // Save quote in data structure 
            QuoteStarted?.Invoke(this, quote);
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