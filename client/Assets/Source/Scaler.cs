using System;
using UnityEngine;
using System.Collections.Generic;

public class OriginalDimensions : MonoBehaviour {
  public Vector3 Position { get; set; }
}

public class Scaler {
  private double kAspectRatio = 1.7;
  private double kStatusBarHeight = 40.0; // TODO: Try and get this from the OS
  private double kWorldWidth = 300.0;
  private double kStandardDpi = 90.0;

  public void Scale(Component component) {
    UpdateMainCamera();
    foreach (Transform transform in component.GetComponentsInChildren<Transform>()) {
      ScaleTransform(transform);
    }

    Dictionary<String, Sprite> spriteMap = new Dictionary<String, Sprite>();
    Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/dpi" + TargetDpi());
    Debug.Log("Screen width " + Screen.width);
    Debug.Log("Screen height " + Screen.height);
    Debug.Log("Target width " + TargetGameWidth());
    Debug.Log("Target height " + TargetGameHeight());
    Debug.Log("Target DPI: " + TargetDpi());
    if (sprites.Length == 0) throw new Exception("Unsupported DPI: " + TargetDpi());
    foreach (Sprite sprite in sprites) {
      spriteMap.Add(sprite.name, sprite);
    }

    foreach (SpriteRenderer spriteRenderer in component.GetComponentsInChildren<SpriteRenderer>()) {
      if (!spriteMap.ContainsKey(spriteRenderer.sprite.name)) {
        throw new Exception("Sprite not found: " + spriteRenderer.sprite.name);
      }
      spriteRenderer.sprite = spriteMap[spriteRenderer.sprite.name];
    }
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
    double cameraHeight = (TargetGameHeight() + kStatusBarHeight) / 2.0f;
    Camera.main.transform.position = new Vector3((float) (TargetGameWidth() / 2.0),
                                                 (float) cameraHeight,
                                                 -10.0f);
    Camera.main.orthographicSize = (float) cameraHeight;
  }

  double TargetGameWidth() {
    return Math.Floor(Math.Min(Screen.width,
        (Screen.height - kStatusBarHeight) / kAspectRatio));
  }

  double TargetGameHeight() {
    return Math.Floor(Math.Min(Screen.height - kStatusBarHeight,
        Screen.width * kAspectRatio));
  }

  string TargetDpi() {
    return "" + ((kStandardDpi * Math.Round(TargetGameWidth())) / kWorldWidth);
  }
}
