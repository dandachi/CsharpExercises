using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingWorkflowExercises.Utils
{
    internal class Quote
    {
        public int QuoteId { get; }
        public CurrencyPair CurrencyPair { get; }

        public Quote(int quoteId, CurrencyPair currencyPair)
        {
            QuoteId = quoteId;
            CurrencyPair = currencyPair;
        }

        public override bool Equals(object obj)
        {
            return obj is Quote quote &&
                   QuoteId == quote.QuoteId &&
                   EqualityComparer<CurrencyPair>.Default.Equals(CurrencyPair, quote.CurrencyPair);
        }

        public override int GetHashCode()
        {
            int hashCode = 2010119904;
            hashCode = hashCode * -1521134295 + QuoteId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<CurrencyPair>.Default.GetHashCode(CurrencyPair);
            return hashCode;
        }

        public override string ToString()
        {
            return $"Quote [Id={QuoteId}, CurrencyPair={CurrencyPair}]";
        }
    }
}
