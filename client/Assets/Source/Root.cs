﻿using UnityEngine;
using strange.extensions.context.impl;

namespace SwapDrop {
  /// <summary>
  /// Top level ContextView, designed to be attached to the RootObject.
  /// </summary>
  public class Root : ContextView {
    void Awake() {
      Screen.fullScreen = false;
      context = new Context(this);
      gameObject.AddComponent<Scaler>();
    }
  }
}