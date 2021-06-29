using System;
using Polly;
using Polly.CircuitBreaker;

namespace WorkerConsumoAPIContagem.Resilience
{
    public static class CircuitBreaker
    {
        public static AsyncCircuitBreakerPolicy CreatePolicy()
        {
            return Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10),
                    onBreak: (_, _) =>
                    {
                        ShowCircuitState("Open (onBreak)", ConsoleColor.Red);
                    },                            
                    onReset: () =>
                    {
                        ShowCircuitState("Closed (onReset)", ConsoleColor.Green);
                    },
                    onHalfOpen: () =>
                    {
                        ShowCircuitState("Half Open (onHalfOpen)", ConsoleColor.Yellow);
                    });
        }

        private static void ShowCircuitState(
            string descStatus, ConsoleColor backgroundColor)
        {
            var previousBackgroundColor = Console.BackgroundColor;
            var previousForegroundColor = Console.ForegroundColor;
            
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            
            Console.Out.WriteLine($" ***** Estado do Circuito: {descStatus} **** ");
            
            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }
    }
}