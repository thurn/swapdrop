using UnityEngine;

namespace SwapDrop {
  /// <summary>
  /// Interface for the Scaler service which can be used to update the size of objects
  /// based on the current device screen size.
  /// </summary>
  public interface IScaler {
    /// <summary>
    /// Modifies the position of the provided transform based on the current game scale.
    /// </summary>
    /// <param name="transform">The transform to scale.</param>
    /// <param name="position"> The position of the transform.</param>
    void ScaleTransform(Transform transform, Vector3 position);

    /// <summary>
    /// Changes the Sprite attached to a spriteRenderer based on the current game scale.
    /// </summary>
    /// <param name="spriteRenderer">Sprite renderer to modify.</param>
    /// <param name="spriteName">The name of the sprite to use.</param>
    void ScaleSpriteRenderer(SpriteRenderer spriteRenderer, string spriteName);

    void DoThis();
  }
}