using Gist2.Extensions.Maths;
using NUnit.Framework;

namespace Gist2.Tests {

    public class Test_Extension_Math {

        [Test]
        public void Quantize() {
            var delta = 1e-2f;

            Assert.AreEqual(2f, 2f.Quantize2(1), delta);
            Assert.AreEqual(2f, 2f.Quantize2(2), delta);
            Assert.AreEqual(2f, 3f.Quantize2(1), delta);

            Assert.AreEqual(-2f, -3f.Quantize2(1));

            Assert.AreEqual(123f, 123f.Quantize10(3), delta);
            Assert.AreEqual(120f, 123f.Quantize10(2), delta);
            Assert.AreEqual(100f, 123f.Quantize10(1), delta);

            Assert.AreEqual(-100f, -123f.Quantize10(1), delta);
        }
    }
}