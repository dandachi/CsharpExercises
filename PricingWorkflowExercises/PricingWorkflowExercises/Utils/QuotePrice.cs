using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingWorkflowExercises.Utils
{
    internal class QuotePrice
    {
        public Quote Quote { get; internal set; }
        public decimal Price { get; internal set; }

        public override string ToString()
        {
            return $"{Quote}: Price= {Price}";
        }
    }
}
