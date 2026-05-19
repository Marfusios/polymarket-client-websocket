using System.Collections.Generic;
using System.Linq;
using Polymarket.Client.Websocket.Exceptions;

namespace Polymarket.Client.Websocket.Validations
{
    internal static class PolyValidations
    {
        public static void ValidateInput<T>(T value, string name)
        {
            if (value == null)
            {
                throw new PolymarketBadInputException($"Argument '{name}' is null");
            }
        }

        public static void ValidateNotEmpty(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new PolymarketBadInputException($"Argument '{name}' is empty");
            }
        }

        public static string[] ValidateArray(IEnumerable<string> values, string name)
        {
            ValidateInput(values, name);

            var array = values.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (array.Length == 0)
            {
                throw new PolymarketBadInputException($"Argument '{name}' must contain at least one value");
            }

            return array;
        }

        public static void ValidateOperation(string operation)
        {
            ValidateNotEmpty(operation, nameof(operation));

            if (operation != "subscribe" && operation != "unsubscribe")
            {
                throw new PolymarketBadInputException("Operation must be 'subscribe' or 'unsubscribe'");
            }
        }
    }
}
