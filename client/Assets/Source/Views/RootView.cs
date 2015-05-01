using UnityEngine;
using strange.extensions.mediation.impl;

namespace SwapDrop.Views {
  public class RootView : View {
    override protected void Start() {
      var background = new GameObject("Background");
      background.transform.parent = transform;
      var renderer = background.AddComponent<SpriteRenderer>();
      Scaler.GetInstance().ScaleSpriteRenderer(renderer, "background");
      Scaler.GetInstance().ScaleTransform(background.transform, new Vector3(-50, -45));
      renderer.sortingOrder = -1;

      var grid = Grid.Instantiate(transform);
      Scaler.GetInstance().ScaleTransform(grid.transform, new Vector3(0, 105));

      var topRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
      topRight.transform.parent = transform;
      topRight.transform.position = new Vector3(300, 510);
      topRight.transform.localScale = new Vector3(10, 10, 1);
      topRight.name = "TopRight";

      var bottomLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
      bottomLeft.transform.parent = transform;
      bottomLeft.transform.localScale = new Vector3(10, 10, 1);
      bottomLeft.name = "BottomLeft";
    }
  }
}