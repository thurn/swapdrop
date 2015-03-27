using UnityEngine;
using System.Collections;

public interface IUserInput {
  bool GetMouseButtonUp(int button);

  bool GetMouseButtonDown(int button);

  Vector3 MousePosition { get; }
}
