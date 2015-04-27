using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using SwapDrop.Models;

namespace SwapDrop.Views {
  /// <summary>
  /// Grid view, renders the primary game board, providing the ability to spawn new gems onto the board and
  /// to detect taps on individual grid cells.
  /// </summary>
  public class Grid : View {
    /// <summary>
    /// Invoked when a grid cell is tapped by the viewer.
    /// </summary>
    public readonly Signal<GridCell> CellTapped = new Signal<GridCell>();
 
    private const int kGridMargin = 4;
    private const int kGridSize = 70;
    private const int kNumberOfSquares = 4;

    private Renderer _renderer;
 
    override protected void Awake() {
      _renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Animate a new gem appearing on the grid.
    /// </summary>
    /// <param name="cell">The cell on which to place the new gem.</param>
    /// <param name="type">The type of gem to spawn.</param>
    public void SpawnGemAtCell(GridCell cell, GemType type) {
      print("Spawn gem at cell: " + cell);
      Gem.Instantiate(transform, type);
    }
 
    void Update() {
      if (Input.GetMouseButtonUp(0) && _renderer.bounds.Contains(Input.mousePosition)) {
        CellTapped.Dispatch(GetTappedCell(Input.mousePosition));
      }
    }

    GridCell GetTappedCell(Vector3 tappedPosition) {
      Vector3 localPosition = transform.InverseTransformPoint(tappedPosition);
      return new GridCell((int) localPosition.x / (kGridSize + kGridMargin),
          (int) localPosition.y / (kGridSize + kGridMargin));
    }
  }
}