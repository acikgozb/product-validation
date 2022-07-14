using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace ProductValidation.UnitTests.Util.TestBase;

public abstract class TestBase<TClassUnderTest>
{
    private readonly IFixture _fixture;
    private readonly IDictionary<Type, Mock> _mockDictionary;

    protected TestBase()
    {
        _mockDictionary = new Dictionary<Type, Mock>();
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    public TClassUnderTest ClassUnderTest => _fixture.Create<TClassUnderTest>();

    public Mock<TDependency> MockFor<TDependency>() where TDependency : class
    {
        var dependencyKey = typeof(TDependency);
        if (_mockDictionary.ContainsKey(dependencyKey))
        {
            return (_mockDictionary[dependencyKey] as Mock<TDependency>)!;
        }

        var mockedDependency = new Mock<TDependency>(MockBehavior.Strict);
        _mockDictionary.Add(dependencyKey, mockedDependency);
        _fixture.Inject(mockedDependency.Object);

        return mockedDependency;
    }
}