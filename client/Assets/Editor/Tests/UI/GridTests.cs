using System;
using NUnit.Framework;
using UnityEngine;

namespace SwapDrop.UI {
  [TestFixture]
  internal class GridTests {
    [Test]
    public void AdditionTest() {
      Assert.That(5, Is.EqualTo(3 + 2));
    }
  }
}