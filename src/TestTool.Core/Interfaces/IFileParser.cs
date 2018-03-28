using System.Collections.Generic;

namespace TestTool.Core
{
    public interface IFileParser
    {
        ITestPlan Parse(string filePath);
        IEnumerable<string> SupportedFormats { get; }
        string Name { get; }
    }
}
