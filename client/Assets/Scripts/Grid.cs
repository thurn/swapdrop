using UnityEngine;
using System.Collections;

namespace SwapDrop {
  public class Grid : MonoBehaviour {

    // Use this for initialization
    void Start() {
      print("Grid position: " + transform.position);
      var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
      print("Grid screen position: " + screenPosition);
      var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
      print("Grid world position: " + worldPosition);
    }
 
    // Update is called once per frame
    void Update() {
  
    }
  }
}