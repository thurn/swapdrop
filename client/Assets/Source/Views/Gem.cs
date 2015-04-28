using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using SwapDrop.Models;

namespace SwapDrop.Views {
  public class Gem : View {
    public static GameObject Instantiate(Transform parent, GemType gemType) {
      var gameObject = new GameObject("Gem");
      gameObject.transform.parent = parent;
      var gem = gameObject.AddComponent<Gem>();
      gem.Init(gemType);
      return gameObject;
    }

    void Init(GemType gemType) {
      var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
      Scaler.GetInstance().ScaleSpriteRenderer(spriteRenderer, GemResourceName(gemType));
    }

    string GemResourceName(GemType type) {
      switch (type) {
      case GemType.BLUE:
        return "blue";
      case GemType.RED:
        return "red";
      }
      throw new UnityException("Unknown gem type: " + type);
    }
  }
}