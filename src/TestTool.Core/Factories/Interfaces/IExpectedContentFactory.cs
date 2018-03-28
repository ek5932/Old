namespace TestTool.Core.Factories
{
    public interface IExpectedContentFactory
    {
        IExpectedContent CreateExpectedScalarContent<T>(T value);
    }
}
