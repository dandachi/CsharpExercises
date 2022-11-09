using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingWorkflowExercises.Utils
{
    internal class CurrencyPair
    {
        public CurrencyPair(string currency1, string currency2)
        {
            Currency1 = currency1;
            Currency2 = currency2;
        }

        public string Currency1 { get; }
        public string Currency2 { get; }

        public override bool Equals(object obj)
        {
            return obj is CurrencyPair pair &&
                   Currency1 == pair.Currency1 &&
                   Currency2 == pair.Currency2;
        }

        public override int GetHashCode()
        {
            int hashCode = -662578599;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Currency1);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Currency2);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Currency1}/{Currency2}";
        }
    }
}
