using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTool.Parser.Excel.Tests
{
    [TestClass]
    public class ExcelFileParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var instance = new ExcelFileParser();
            instance.Parse(@"C:\Users\Paul\Documents\Dev\TestTool\Bdd.xlsx");
        }
    }
}
