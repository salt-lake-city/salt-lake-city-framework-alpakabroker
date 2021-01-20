namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
    internal class GeneratedSource
    {
        public GeneratedSource(string sourceCode, string fileName)
        {
            SourceCode = sourceCode;
            FileName = fileName;
        }

        public string SourceCode { get; set; }
        public string FileName { get; set; }
    }
}