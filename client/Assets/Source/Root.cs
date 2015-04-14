using UnityEngine;
using strange.extensions.context.impl;

namespace SwapDrop {
  /// <summary>
  /// Top level ContextView, designed to be attached to the RootObject.
  /// </summary>
  public class Root : ContextView {
    void Awake() {
      // We do this instead of setting full screen directly in order to show the status bar
      // properly on Android.
      Screen.SetResolution(Screen.width, Screen.height, false);
      context = new Context(this);

      Camera.main.transform.position = new Vector3(Screen.width / 2.0f,
                                                   Screen.height / 2.0f,
                                                   -10.0f);
      Camera.main.orthographicSize = Screen.height / 2.0f;

      Scaler scaler = new Scaler();
      foreach (Transform transform in GetComponentsInChildren<Transform>()) {
        scaler.Scale(transform);
      }
    }
  }
}