using PricingWorkflowExercises.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PricingWorkflowExercises.Pricer
{
    internal class SpotPricer : IPricer, IDisposable
    {
        private readonly List<Quote> _quotes;
        private object _lock = new object();
        private readonly Timer _pricingTimer;
        public event EventHandler<QuotePrice> PriceUpdated;
        private const int TickInSeconds = 2;

        public SpotPricer()
        {
            _pricingTimer = new Timer(OnTimerElapsed, null, 0, TickInSeconds);
        }

        private void OnTimerElapsed(object state)
        {
            lock (_lock)
            {
                foreach(var quote in _quotes)
                {
                    var quotePrice = PriceQuote(quote);
                    PriceUpdated?.Invoke(this, quotePrice);
                }
            }
        }

        public void StartPrice(Quote quote)
        {
            lock (_lock)
            {
                _quotes.Add(quote);
            }
        }

        public void StopPrice(Quote quote)
        {
            lock (_lock)
            {
                _quotes.Remove(quote);
            }
        }

        private QuotePrice PriceQuote(Quote quote)
        {
            var random = new Random();
            var variation = (((decimal)random.NextDouble()) * 2.0m - 1.0m)/ 100.0m;
            var newPrice = Math.Round(GetCurrencyPairCoefficient(quote.CurrencyPair) + variation, 5);
            return new QuotePrice()
            {
                Quote = quote,
                Price = newPrice,
            };
        }

        private decimal GetCurrencyPairCoefficient(CurrencyPair currencyPair)
        {
            return GetCurrencyCoefficient(currencyPair.Currency1) / GetCurrencyCoefficient(currencyPair.Currency2);
        }
        
        private decimal GetCurrencyCoefficient(string currency)
        {
            switch(currency)
            {
                case "EUR": return 0.99m;
                case "USD": return 1.0m;
                case "CAD": return 0.74m;
                case "GBP": return 1.13m;
                case "JPY": return 0.0068m;
            }
            return 1.0m;
        }
        public void Dispose()
        {
            _pricingTimer.Dispose();
        }
    }
}
