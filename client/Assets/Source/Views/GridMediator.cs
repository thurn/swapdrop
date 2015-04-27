using System;
using UnityEngine;
using strange.extensions.mediation.impl;
using SwapDrop.Models;

namespace SwapDrop.Views {
  public class GridMediator : Mediator {
    [Inject]
    public Grid grid { get; set; }

    public override void OnRegister() {
      grid.CellTapped.AddListener(cell => grid.SpawnGemAtCell(cell, GemType.BLUE));
    }
  }
}