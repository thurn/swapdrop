using UnityEngine;
using System.Collections;

namespace SwapDrop {
  public class Grid : MonoBehaviour {
 
    private const int GridMargin = 4;
    private const int GridSize = 70;
    private const int NumberOfSquares = 4;
 
    // Use this for initialization
    void Start() {
      var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
      print("Grid screen position: " + screenPosition);
      var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
      print("Grid world position: " + worldPosition);
      var bounds = renderer.bounds;
      print("Grid bounds: " + bounds);

      for (int i = 0; i < NumberOfSquares; ++i) {
        for (int j = 0; j < NumberOfSquares; ++j) {
          AddTapRecognizerForCell(i, j);
        }
      }
    }

    void Update() {
      if (Input.touchCount > 0) {
        Debug.Log(Input.GetTouch(0).phase);
      }
    }
 
    private void AddTapRecognizerForCell(int column, int row) {
      // A grid cell at column x and row y has x previous squares before it and
      // x + 1 margins.
      var rect = new Rect((column * GridSize) + ((column + 1) * GridMargin),
                            105 + (row * GridSize) + ((row + 1) * GridMargin),
                            GridSize,
                            GridSize);
    }
  }
}