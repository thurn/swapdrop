using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using SwapDrop.Models;

namespace SwapDrop.Views {
  public class Gem : View {
    public static Gem Instantiate(Transform parent, GemType gemType) {
      var gameObject = new GameObject("Gem");
      gameObject.transform.parent = parent;
      var gem = gameObject.AddComponent<Gem>();
      gem.Init(gemType);
      return gem;
    }

    private GemType _gemType;

    void Init(GemType gemType) {
      _gemType = gemType;
    }
  }
}