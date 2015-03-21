using UnityEngine;
using System.Collections;

namespace SwapDrop.UI {
  public interface IGrid {
    void SpawnGemAtCell(GridCell cell, GemType type);
  }

  public class Grid : MonoBehaviour, IGrid {
    private const int GridMargin = 4;
    private const int GridSize = 70;
    private const int NumberOfSquares = 4;

    private readonly GridController _gridController;

    public Grid() {
      _gridController = new GridController(this);
    }
 
    // Use this for initialization
    void Start() {
      var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
      print("Grid screen position: " + screenPosition);
      var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
      print("Grid world position: " + worldPosition);
      var bounds = renderer.bounds;
      print("Grid bounds: " + bounds);
    }
 
    void Update() {
      if (Input.GetMouseButtonUp(0) && renderer.bounds.Contains(Input.mousePosition)) {
        _gridController.OnCellTapped(GetTappedCell(Input.mousePosition));
      }
    }

    private GridCell GetTappedCell(Vector3 tappedPosition) {
      Vector3 localPosition = transform.InverseTransformPoint(tappedPosition);
      return new GridCell((int) localPosition.x / (GridSize + GridMargin),
          (int) localPosition.y / (GridSize + GridMargin));
    }

    public void SpawnGemAtCell(GridCell cell, GemType type) {
      print("Spawn gem at cell: " + cell);
    }
  }
}