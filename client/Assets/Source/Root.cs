using UnityEngine;
using strange.extensions.context.impl;

namespace SwapDrop {
  /// <summary>
  /// Top level ContextView, designed to be attached to the RootObject.
  /// </summary>
  public class Root : ContextView {
    void Awake() {
      Screen.fullScreen = false;
      context = new Context(this);

      // TODO: Get this from the device
      int statusBarHeight = Application.platform == RuntimePlatform.IPhonePlayer ? 40 : 0;
      _scaler = new Scaler(statusBarHeight);
    }

    private DeviceOrientation deviceOrientation = DeviceOrientation.Unknown;
    private Scaler _scaler;

    public void Update() {
      if (Input.deviceOrientation != deviceOrientation
          && Input.deviceOrientation != DeviceOrientation.FaceUp
          && Input.deviceOrientation != DeviceOrientation.FaceDown) {
        deviceOrientation = Input.deviceOrientation;
        _scaler.Scale(this);
      }
    }
  }
}