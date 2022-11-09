using currus.Extensions;

namespace currus.Tests.ExtensionMethod;

[TestFixture]
internal class IntArrayExtensionsTests
{
    private int[] array = { 1, 2, 3 };
    private int[] emptyArray = { };
    [Test]
    public void IsLengthLessThanOrEqualTo_InputWrong_ShouldReturnFalse()
    {
        var expectedValue = false;
        var maxValue = 2;
        Assert.That(expectedValue, Is.EqualTo(array.IsLengthLessThanOrEqualTo(maxValue)));
        maxValue = 0;
        Assert.That(expectedValue, Is.EqualTo(emptyArray.IsLengthLessThanOrEqualTo(maxValue)));
    }

    [Test]
    public void IsLengthLessThanOrEqualTo_InputValid_ShouldReturnTrue()
    {
        var expectedValue = true;
        var maxValue = 4;
        Assert.That(expectedValue, Is.EqualTo(array.IsLengthLessThanOrEqualTo(maxValue)));
        maxValue = 3;
        Assert.That(expectedValue, Is.EqualTo(array.IsLengthLessThanOrEqualTo(maxValue)));
    }
}