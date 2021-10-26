using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTank : MonoBehaviour {
  public float forceMagnitude = 15;
  public float torqueMagnitude = 50f;

  Rigidbody rbody;
  Vector2 moveDirection;

  private void Start() {
    rbody = GetComponent<Rigidbody>();
  }

  public void OnMove(Vector2 input) {
    moveDirection = input;
  }

  public void OnDisconnect() {
    moveDirection = Vector2.zero;
  }

  private void FixedUpdate() {
    rbody.AddRelativeForce(
      Vector3.forward *
      moveDirection.y *
      forceMagnitude
    );

    rbody.AddRelativeTorque(
      Vector3.up *
      moveDirection.x *
      torqueMagnitude,
      ForceMode.Acceleration
    );

  }

}
