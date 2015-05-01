using UnityEngine;
using strange.extensions.context.impl;

namespace SwapDrop {
  /// <summary>
  /// Top level ContextView, designed to be attached to the RootObject.
  /// </summary>
  public class Root : ContextView {
    void Awake() {
      Screen.fullScreen = false;
      gameObject.AddComponent<Scaler>();
      context = new SwapDropContext(this);
      gameObject.AddComponent<SwapDrop.Views.RootView>();

      Camera.main.transform.position = new Vector3(150, 255, -10);
      Camera.main.orthographicSize = 255;
    }
  }
}