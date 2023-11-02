
namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }
        public void FalseTest()
        {
            Assert.False(false);
        }
    }
}