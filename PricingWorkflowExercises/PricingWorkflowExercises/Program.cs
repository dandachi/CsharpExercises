using PricingWorkflowExercises.Utils;
using PricingWorkflowExercises.QuoteSource;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using PricingWorkflowExercises.Pricer;

namespace PricingWorkflowExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var quotePlatform = new QuotePlatform();
            var spotPricer = new SpotPricer();
            var pricingWorkflowRunner = new PricingWorkflowRunner(quotePlatform, spotPricer);

            quotePlatform.Start(pricingWorkflowRunner);
            quotePlatform.Stop();
        }
    }
}
