using UnityEngine;
using System.Collections;

public class InputFacade : IUserInput {
  public bool GetMouseButtonDown(int buttonNumber) {
    return Input.GetMouseButtonDown(buttonNumber);
  }

  public bool GetMouseButtonUp(int buttonNumber) {
    return Input.GetMouseButtonUp(buttonNumber);
  }

  public Vector3 MousePosition {
    get {
      return Input.mousePosition;
    }
  }
}
