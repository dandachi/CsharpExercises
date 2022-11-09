using PricingWorkflowExercises.Utils;
using System;

namespace PricingWorkflowExercises.Pricer
{
    internal interface IPricer
    {
        event EventHandler<QuotePrice> PriceUpdated;
        void StartPrice(Quote quote);
        void StopPrice(Quote quote);
    }
}