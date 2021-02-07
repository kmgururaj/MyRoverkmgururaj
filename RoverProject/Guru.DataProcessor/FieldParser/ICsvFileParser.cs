using System;
using System.Collections.Generic;

namespace Guru.DataProcessor.FieldParser
{
    /// <summary>
    /// CSV FileParser Interface
    /// </summary>
    public interface ICsvFileParser : IDisposable
    {
        /// <summary>
        /// Read Fields
        /// </summary>
        /// <returns>string collection of columns</returns>
        IEnumerable<string[]> ReadFields();

        /// <summary>
        /// Line Number
        /// </summary>
        long LineNumber { get; set; }

        string FilePath { get; }
    }
}
