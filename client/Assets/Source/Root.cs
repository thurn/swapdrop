using UnityEngine;
using strange.extensions.context.impl;

namespace SwapDrop {
  /// <summary>
  /// Top level ContextView, designed to be attached to the RootObject.
  /// </summary>
  public class Root : ContextView {
    void Awake() {
      Screen.fullScreen = false;
      AndroidPlugin androidPlugin = new AndroidPlugin();
      androidPlugin.Init();
      androidPlugin.ShowStatusBar();

      context = new Context(this);

      Scaler scaler = new Scaler(0);
      scaler.Scale(this);
    }
  }
}