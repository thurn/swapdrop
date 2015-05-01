using System;
using UnityEngine;
using System.Collections.Generic;

namespace SwapDrop {
  public class OriginalDimensions : MonoBehaviour {
    public Vector3 Position { get; set; }
  }

  public class Scaler : MonoBehaviour, IScaler {
    private static IScaler _instance;

    public static IScaler GetInstance() {
      return _instance;
    }

    private const double kAspectRatio = 1.7;
    private const double kWorldWidth = 300.0;
    private const double kStandardDpi = 90.0;

    private int _statusBarHeight;
    private DeviceOrientation _deviceOrientation = DeviceOrientation.Unknown;
    // Map from DPI -> Sprite Name -> Sprite
    private Dictionary<string, Dictionary<string, Sprite>> _spriteCache;
    
    public void Awake() {
      _instance = this;
      // TODO: Get this from the device
      _statusBarHeight = Application.platform == RuntimePlatform.IPhonePlayer ? 40 : 0;
      _spriteCache = new Dictionary<string, Dictionary<string, Sprite>>();
    }

    public void Update() {
      var newOrientation = Input.deviceOrientation;
      if (newOrientation != _deviceOrientation
          && newOrientation != DeviceOrientation.FaceUp
          && newOrientation != DeviceOrientation.FaceDown) {
        // Always rotate counterclockwise for landscape
        Scale(this, IsLandscape() ? -90 : 0);
        _deviceOrientation = newOrientation;
      }
    }

    public void DoThis() {
      Debug.Log("DoThis");
    }

    public void Scale(MonoBehaviour component, float rotationAngle) {
      UpdateMainCamera(rotationAngle);
      foreach (Transform transform in component.GetComponentsInChildren<Transform>()) {
        ScaleTransform(transform, transform.position);
      }

      foreach (SpriteRenderer spriteRenderer in component.GetComponentsInChildren<SpriteRenderer>()) {
        ScaleSpriteRenderer(spriteRenderer, spriteRenderer.sprite.name);
      }
    }
 
    void LoadSpritesForDpi(string dpi) {
      var sprites = Resources.LoadAll<Sprite>("Sprites/dpi" + dpi);
      if (sprites.Length == 0) {
        Debug.Log("Unsupported DPI");
        return;
      }
      var result = new Dictionary<string, Sprite>();
      foreach (Sprite sprite in sprites) {
        result.Add(sprite.name, sprite);
      }
      _spriteCache.Add(dpi, result);
    }

    public void ScaleSpriteRenderer(SpriteRenderer spriteRenderer, string spriteName) {
      var dpi = TargetDpi();
      if (!_spriteCache.ContainsKey(dpi)) {
        LoadSpritesForDpi(dpi);
      }
      var spriteMap = _spriteCache[dpi];
      if (!spriteMap.ContainsKey(spriteName)) {
        throw new Exception("Sprite not found: " + spriteName);
      }
      spriteRenderer.sprite = spriteMap[spriteName];
    }

    public void ScaleTransform(Transform transform, Vector3 position) {
      var scaleFactor = TargetGameWidth() / kWorldWidth;
      transform.localPosition = position;
   
      OriginalDimensions dimensions = transform.gameObject.GetComponent<OriginalDimensions>();
      if (dimensions == null) {
        dimensions = transform.gameObject.AddComponent<OriginalDimensions>();
        dimensions.Position = transform.position;
      }

      transform.position = new Vector3((float) (dimensions.Position.x * scaleFactor),
                                       (float) (dimensions.Position.y * scaleFactor),
                                       dimensions.Position.z);
    }

    void UpdateMainCamera(float rotationAngle) {
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
      
      Camera.main.transform.rotation = Quaternion.identity;
      Camera.main.transform.RotateAround(
        new Vector3(
        Camera.main.transform.position.x,
        Camera.main.transform.position.y),
        Vector3.forward,
        rotationAngle);
    }
 
    double Width() {
      var result = Math.Min(Screen.width, Screen.height);
      return IsLandscape() ? result - _statusBarHeight : result;
    }

    double Height() {
      var result = Math.Max(Screen.width, Screen.height);
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