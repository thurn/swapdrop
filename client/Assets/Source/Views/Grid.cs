using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using SwapDrop.Models;

namespace SwapDrop.Views {
  /// <summary>
  /// Possible colors of gem to spawn, one per player.
  /// </summary>
  public enum GemType {
    BLUE,
    RED
  }

  /// <summary>
  /// Grid view, renders the primary game board, providing the ability to spawn new gems onto the board and
  /// to detect taps on individual grid cells.
  /// </summary>
  public class Grid : View {
    /// <summary>
    /// Invoked when a grid cell is tapped by the viewer.
    /// </summary>
    internal readonly Signal<GridCell> CellTapped = new Signal<GridCell>();
 
    private const int GridMargin = 4;
    private const int GridSize = 70;
    private const int NumberOfSquares = 4;
 
    /// <summary>
    /// Initialize this instance.
    /// </summary>
    internal void Init() {
      var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
      print("Grid screen position: " + screenPosition);
      var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
      print("Grid world position: " + worldPosition);
      var bounds = renderer.bounds;
      print("Grid bounds: " + bounds);
    }

    /// <summary>
    /// Animate a new gem appearing on the grid.
    /// </summary>
    /// <param name="cell">The cell on which to place the new gem.</param>
    /// <param name="type">The type of gem to spawn.</param>
    internal void SpawnGemAtCell(GridCell cell, GemType type) {
      print("Spawn gem at cell: " + cell);
    }
 
    void Update() {
      if (Input.GetMouseButtonUp(0) && renderer.bounds.Contains(Input.mousePosition)) {
        CellTapped.Dispatch(GetTappedCell(Input.mousePosition));
      }
    }

    GridCell GetTappedCell(Vector3 tappedPosition) {
      Vector3 localPosition = transform.InverseTransformPoint(tappedPosition);
      return new GridCell((int) localPosition.x / (GridSize + GridMargin),
          (int) localPosition.y / (GridSize + GridMargin));
    }
  }
}