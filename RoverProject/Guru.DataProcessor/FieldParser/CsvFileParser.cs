using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace Guru.DataProcessor.FieldParser
{
    public class CsvFileParser : ICsvFileParser
    {
        private TextFieldParser textFieldParser;

        public string FilePath { get; private set; }

        public long LineNumber { get; set; }

        /// <summary>
        /// CSV column delimiters
        /// </summary>
        private const string delimiters = "|";

        private bool isDisposed;

        public CsvFileParser(string filePath)
        {
            textFieldParser = new TextFieldParser(filePath);
            textFieldParser.TextFieldType = FieldType.Delimited;
            textFieldParser.SetDelimiters(delimiters);
            FilePath = filePath;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !isDisposed)
            {
                return;
            }
            if (textFieldParser != null)
            {
                textFieldParser.Dispose();
                textFieldParser = null;
            }
            FilePath = null;

            isDisposed = true;
        }

        /// <summary>
        /// Read Fields
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string[]> ReadFields()
        {
            while (!textFieldParser.EndOfData)
            {
                LineNumber = textFieldParser.LineNumber;
                yield return textFieldParser.ReadFields();
            }
        }
    }
}
