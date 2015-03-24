using UnityEngine;
using System.Collections;

namespace SwapDrop.Models {
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
}