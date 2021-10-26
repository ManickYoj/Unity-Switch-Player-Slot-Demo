using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Must be placed on the designated player prefab that is spawned
// in by the multiplayer manager in order to be owned by the client
[RequireComponent(typeof(PlayerInput))]
public class SlotController : MonoBehaviour {
  private Slot slot;
  private PlayerInput playerInput;

  void Start() {
    this.playerInput = this.GetComponent<PlayerInput>();
    this.playerInput.enabled = true;
    SlotRegistry.Instance.GetNewSlot(this);
  }

  public void Connect(Slot slot) {
    foreach(Camera c in Camera.allCameras) c.gameObject.SetActive(false);
    this.slot = slot;
    this.slot.slotCamera.gameObject.SetActive(true);
  }

  public void Disconnect(Slot slot) {
    slot.SendCommand("OnDisconnect");
  }

  public void OnSwitchSlot(InputValue inputValue) {
    SlotRegistry.Instance.GetNewSlot(this);
  }

  public void OnMove(InputValue inputValue) {
    if (slot != null) slot.SendCommand("OnMove", inputValue.Get<Vector2>());
  }

  public void OnFire(InputValue inputValue) {
    if (slot != null) slot.SendCommand("OnFire");
  }
}
