using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Must be placed on the designated player prefab that is spawned
// in by the multiplayer manager in order to be owned by the client
[RequireComponent(typeof(PlayerInput))]
public class Transmitter : MonoBehaviour {
  private Receiver receiver;
  private PlayerInput playerInput;

  void Start() {
    this.playerInput = this.GetComponent<PlayerInput>();
    this.playerInput.enabled = true;
    Registry.GetNewReceiver(this);
  }

  public void Connect(Receiver receiver) {
    foreach(Camera c in Camera.allCameras) c.gameObject.SetActive(false);
    this.receiver = receiver;
    this.receiver.receiverCamera.gameObject.SetActive(true);
  }

  public void Disconnect(Receiver receiver) {
    receiver.SendCommand("OnDisconnect");
  }

  public void OnSwitchReceiver(InputValue inputValue) {
    Registry.GetNewReceiver(this);
  }

  public void OnMove(InputValue inputValue) {
    if (receiver != null) receiver.SendCommand("OnMove", inputValue.Get<Vector2>());
  }

  public void OnFire(InputValue inputValue) {
    if (receiver != null) receiver.SendCommand("OnFire");
  }
}
