using System;
using NUnit.Framework;
using UnityEngine;

namespace SwapDrop.UI {
  [TestFixture]
  internal class GridTests {
    [Test]
    public void AdditionTest() {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<Grid>();
      Assert.That(5, Is.EqualTo(3 + 2));
    }
  }
}