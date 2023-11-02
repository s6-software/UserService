using API.Controllers;
namespace Test
{
    public class IntegrationTest
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