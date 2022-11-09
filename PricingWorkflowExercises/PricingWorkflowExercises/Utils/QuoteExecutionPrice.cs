using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingWorkflowExercises.Utils
{
    internal class QuoteExecutionPrice
    {
        public Quote Quote { get; internal set; }
        public decimal ExecutionPrice { get; internal set; }

        public override string ToString()
        {
            return $"{Quote}: Execution Price= {ExecutionPrice}";
        }
    }
}
