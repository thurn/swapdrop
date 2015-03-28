using UnityEngine;
using NUnit.Framework;

namespace SwapDrop.Views {
  [TestFixture]
  public class GridTests {
    [Test]
    public void TestAddition() {
      Assert.That(2 + 2, Is.EqualTo(4));
    }
  }
}