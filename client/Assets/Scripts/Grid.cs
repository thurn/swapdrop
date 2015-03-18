using UnityEngine;
using System.Collections;

namespace SwapDrop {
  public sealed class GridCellTappedEvent {
    private readonly int _column;
    private readonly int _row;
 
    public int Column { get { return _column; } }
    public int Row { get { return _row; } }
 
    public GridCellTappedEvent(int column, int row) {
      _column = column;
      _row = row;
    }

    public override string ToString() {
      return string.Format("GridCellTappedEvent [row={0}, column={1}]", _row, _column);
    }
  }

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
    }

    void Update() {
      if (Input.GetMouseButtonUp(0) && renderer.bounds.Contains(Input.mousePosition)) {
        print("Click " + GetTappedCell(Input.mousePosition));
      }
    }

    private GridCellTappedEvent GetTappedCell(Vector3 tappedPosition) {
      Vector3 localPosition = transform.InverseTransformPoint(tappedPosition);
      return new GridCellTappedEvent((int) localPosition.x / (GridSize + GridMargin),
          (int) localPosition.y / (GridSize + GridMargin));
    }
  }
}