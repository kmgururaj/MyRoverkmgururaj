using System.Collections.Generic;

namespace Guru.DataProcessor.DataProcessing
{
    /// <summary>
    /// 
    /// </summary>
    internal class InitialCommandProcessor
    {
        private readonly string commandInternal;

        public InitialCommandProcessor(string command)
        {
            commandInternal = command;
        }

        public IEnumerable<char> Start()
        {
            foreach (var item in commandInternal)
            {
                if (item == ' ')
                {
                    continue;
                }

                yield return char.ToUpper(item);
            }
        }
    }
}
