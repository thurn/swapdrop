﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace SwapDrop {
  public class OriginalDimensions : MonoBehaviour {
    public Vector3 Position { get; set; }
  }

  public class Scaler : MonoBehaviour {
    private const double kAspectRatio = 1.7;
    private const double kWorldWidth = 300.0;
    private const double kStandardDpi = 90.0;

    private int _statusBarHeight;
    private DeviceOrientation _deviceOrientation = DeviceOrientation.Unknown;
    
    public void Start() {
      // TODO: Get this from the device
      _statusBarHeight = Application.platform == RuntimePlatform.IPhonePlayer ? 40 : 0;
    }

    public void Update() {
      DeviceOrientation newOrientation = Input.deviceOrientation;
      if (newOrientation != _deviceOrientation
          && newOrientation != DeviceOrientation.FaceUp
          && newOrientation != DeviceOrientation.FaceDown) {
        // Always rotate counterclockwise for landscape
        Scale(this, IsLandscape() ? -90 : 0);
        _deviceOrientation = newOrientation;
      }
    }

    public void Scale(MonoBehaviour component, float rotationAngle) {
      UpdateMainCamera();
      foreach (Transform transform in component.GetComponentsInChildren<Transform>()) {
        ScaleTransform(transform);
      }

      Dictionary<String, Sprite> spriteMap = new Dictionary<String, Sprite>();
      Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/dpi" + TargetDpi());
      if (sprites.Length == 0) {
        Debug.Log("Unsupported DPI");
        return;
      }
      foreach (Sprite sprite in sprites) {
        spriteMap.Add(sprite.name, sprite);
      }

      foreach (SpriteRenderer spriteRenderer in component.GetComponentsInChildren<SpriteRenderer>()) {
        if (!spriteMap.ContainsKey(spriteRenderer.sprite.name)) {
          throw new Exception("Sprite not found: " + spriteRenderer.sprite.name);
        }
        spriteRenderer.sprite = spriteMap[spriteRenderer.sprite.name];
      }

      Camera.main.transform.rotation = Quaternion.identity;
      Camera.main.transform.RotateAround(
          new Vector3(
              Camera.main.transform.position.x,
              Camera.main.transform.position.y),
          Vector3.forward,
          rotationAngle);
    }

    void ScaleTransform(Transform transform) {
      double scaleFactor = TargetGameWidth() / kWorldWidth;
   
      OriginalDimensions dimensions = transform.gameObject.GetComponent<OriginalDimensions>();
      if (dimensions == null) {
        dimensions = transform.gameObject.AddComponent<OriginalDimensions>() as OriginalDimensions;
        dimensions.Position = transform.position;
      }

      transform.position = new Vector3((float) (dimensions.Position.x * scaleFactor),
                                       (float) (dimensions.Position.y * scaleFactor),
                                       dimensions.Position.z);
    }

    void UpdateMainCamera() {
      if (IsLandscape()) {
        float cameraX = (float) ((TargetGameWidth() + _statusBarHeight) / 2.0f);
        Camera.main.transform.position = new Vector3(cameraX,
                                                     (float) (TargetGameHeight() / 2.0),
                                                     -10.0f);
        Camera.main.orthographicSize = cameraX;
      } else {
        float cameraY = (float) ((TargetGameHeight() + _statusBarHeight) / 2.0f);
        Camera.main.transform.position = new Vector3((float) (TargetGameWidth() / 2.0),
                                                     cameraY,
                                                     -10.0f);
        Camera.main.orthographicSize = cameraY;
      }
    }
 
    double Width() {
      int result = Math.Min(Screen.width, Screen.height);
      return IsLandscape() ? result - _statusBarHeight : result;
    }

    double Height() {
      int result = Math.Max(Screen.width, Screen.height);
      return IsLandscape() ? result : result - _statusBarHeight;
    }

    double TargetGameWidth() {
      return Math.Floor(Math.Min(Width(), Height() / kAspectRatio));
    }

    double TargetGameHeight() {
      return Math.Floor(Math.Min(Height(),  Width() * kAspectRatio));
    }

    string TargetDpi() {
      return "" + ((kStandardDpi * Math.Round(TargetGameWidth())) / kWorldWidth);
    }

    bool IsLandscape() {
        return Input.deviceOrientation == DeviceOrientation.LandscapeLeft
          || Input.deviceOrientation == DeviceOrientation.LandscapeRight;
    }
  }

}