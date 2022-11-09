using PricingWorkflowExercises.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace PricingWorkflowExercises.QuoteSource
{
    internal class QuotePlatform : IQuotePlatform
    {
        private const string ScenarioFilePath = @".\SmallScenario.txt";
        private IPricingWorkflowRunner WorkflowRunner;
        private readonly Dictionary<int, Quote> ProcessingQuotes = new Dictionary<int, Quote>();
        private readonly ConcurrentDictionary<int, decimal> QuotePrices = new ConcurrentDictionary<int, decimal>();

        public event EventHandler<Quote> QuoteRequest;
        public event EventHandler<QuoteExecutionPrice> ExecutionRequest;
        public event EventHandler<Quote> StopRequest;
        public event EventHandler StopAllRequests;


        public void Start(IPricingWorkflowRunner workflowRunner)
        {
            WorkflowRunner = workflowRunner;
            WorkflowRunner.QuotePriceUpdated += WorkflowRunner_QuotePriceUpdated;
            WorkflowRunner.QuoteStarted += WorkflowRunner_QuoteStarted;
            WorkflowRunner.QuoteExecutionFailed += WorkflowRunner_QuoteExecutionFailed;
            WorkflowRunner.QuoteExecutionSuccessful += WorkflowRunner_QuoteExecutionSuccessful;
            WorkflowRunner.QuoteStopped += WorkflowRunner_QuoteStopped;
            WorkflowRunner.AllQuotesStopped += WorkflowRunner_AllQuotesStopped;

            using (var fileReader = File.OpenText(ScenarioFilePath))
            {
                var scenarioProcessSpeed = int.Parse(fileReader.ReadLine());
                while (!fileReader.EndOfStream)
                {
                    var nextLine = fileReader.ReadLine();
                    Process(nextLine);
                    Thread.Sleep(scenarioProcessSpeed);
                }
            }
        }

        public void Stop()
        {
            StopAllRequests?.Invoke(this, EventArgs.Empty);

            WorkflowRunner.QuotePriceUpdated -= WorkflowRunner_QuotePriceUpdated;
            WorkflowRunner.QuoteStarted -= WorkflowRunner_QuoteStarted;
            WorkflowRunner.QuoteExecutionFailed -= WorkflowRunner_QuoteExecutionFailed;
            WorkflowRunner.QuoteExecutionSuccessful -= WorkflowRunner_QuoteExecutionSuccessful;
            WorkflowRunner.QuoteStopped -= WorkflowRunner_QuoteStopped;
            WorkflowRunner.AllQuotesStopped -= WorkflowRunner_AllQuotesStopped;

        }

        private void WorkflowRunner_AllQuotesStopped(object sender, EventArgs e)
        {
            Console.WriteLine($"All quotes have been stopped!");
        }

        private void WorkflowRunner_QuoteStopped(object sender, Quote e)
        {
            Console.WriteLine($"Stopped {e}");
        }

        private void WorkflowRunner_QuoteExecutionSuccessful(object sender, QuoteExecutionPrice e)
        {
            Console.WriteLine($"Execution successful for {e}");
        }

        private void WorkflowRunner_QuoteExecutionFailed(object sender, QuoteExecutionPrice e)
        {
            Console.WriteLine($"Execution failed for {e}");
        }

        private void WorkflowRunner_QuoteStarted(object sender, Quote e)
        {
            Console.WriteLine($"Started {e}");
        }

        private void WorkflowRunner_QuotePriceUpdated(object sender, QuotePrice e)
        {
            QuotePrices.AddOrUpdate(e.Quote.QuoteId, (k) => e.Price, (k, v) => e.Price);
            Console.WriteLine($"Price update for {e}");
        }

        private void Process(string nextLine)
        {
            //Examples:
            // 1000
            // CREATE 1 EUR USD
            // EXECUTE 1 0.001
            // STOP 1
            // WAIT
            // END
            var splittedLine = nextLine.Split(' ');

            switch (splittedLine[0])
            {
                case "CREATE":
                    var createQuoteId = int.Parse(splittedLine[1]);
                    var currency1 = splittedLine[2];
                    var currency2 = splittedLine[3];
                    var quote = new Quote(createQuoteId, new CurrencyPair(currency1, currency2));
                    ProcessingQuotes.Add(createQuoteId, quote);
                    QuoteRequest?.Invoke(this, quote);
                    break;
                case "EXECUTE":
                    var executeQuoteId = int.Parse(splittedLine[1]);
                    var executionDelta = decimal.Parse(splittedLine[2], CultureInfo.InvariantCulture);
                    if (!QuotePrices.TryGetValue(executeQuoteId, out var lastPrice))
                    {
                        lastPrice = 0;
                    }
                    QuoteExecutionPrice quoteExecutionPrice = new QuoteExecutionPrice()
                    {
                        Quote = ProcessingQuotes[executeQuoteId],
                        ExecutionPrice = lastPrice + executionDelta
                    };
                    ProcessingQuotes.Remove(executeQuoteId);
                    ExecutionRequest?.Invoke(this, quoteExecutionPrice);
                    break;
                case "STOP":
                    var stopQuoteId = int.Parse(splittedLine[1]);
                    var stopQuote = ProcessingQuotes[stopQuoteId];
                    ProcessingQuotes.Remove(stopQuoteId);
                    StopRequest(this, stopQuote);
                    break;
                case "WAIT":
                    break;
                case "END":
                    ProcessingQuotes.Clear();
                    StopAllRequests?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    throw new ArgumentException("Can't process scenario, unknown command");
            }
        }
    }
}
