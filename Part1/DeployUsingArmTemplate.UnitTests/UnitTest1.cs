using Microsoft.VisualStudio.TestTools.UnitTesting;
using a.Utilities;
using DeployUsingARMTemplate;

namespace DeployUsingArmTemplate.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RandomName_ReturnsRandomString_with_prefix_bu_10characterslong()
        {
            var resourceNamer = new ResourceNamer("");

            var randomName = resourceNamer.RandomName("bu", 10);
            var prefix = "bu";
            var checkLength = false;

            if (randomName.Length == 10)
            {
                checkLength = true;
            }

            StringAssert.StartsWith(randomName, prefix);
            Assert.IsTrue(checkLength);

        }

        [TestMethod]

        public void GetArmTemplate_returning_string()
        {
            var testString = Utilities.GetArmTemplate("ArmTemplate.json");
            var result = false;
            if (!string.IsNullOrEmpty(testString))
            {
                result = true;
            }

            Assert.IsTrue(result);
        }
    }
}
