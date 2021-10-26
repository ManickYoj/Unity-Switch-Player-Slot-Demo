using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate : MonoBehaviour {
  [SerializeField] float horizontalRotationSpeed = 1f;
  [SerializeField] float verticalRotationSpeed = 0;

  Vector2 moveDirection;

  public void OnMove(Vector2 input) {
    moveDirection = input;
  }

  public void OnDisconnect() {
    moveDirection = Vector2.zero;
  }

  private void FixedUpdate() {
    this.transform.Rotate(
      moveDirection.y * verticalRotationSpeed,
      0,
      moveDirection.x * horizontalRotationSpeed
    );
  }
}
