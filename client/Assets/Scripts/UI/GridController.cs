using UnityEngine;
using System.Collections;

namespace SwapDrop.UI {
  public enum GemType {
    BLUE,
    RED
  }

  public sealed class GridCell {
    private readonly int _column;
    private readonly int _row;
    
    public int Column { get { return _column; } }
    public int Row { get { return _row; } }
    
    public GridCell(int column, int row) {
      _column = column;
      _row = row;
    }
    
    public override string ToString() {
      return string.Format("GridCell [row={0}, column={1}]", _row, _column);
    }
  }

  public class GridController {

    private readonly IGrid _grid;

    public GridController(IGrid grid) {
      _grid = grid;
    }

    public void OnCellTapped(GridCell cell) {
      _grid.SpawnGemAtCell(cell, GemType.BLUE);
    }
  }
}