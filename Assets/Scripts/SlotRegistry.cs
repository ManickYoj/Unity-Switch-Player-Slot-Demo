using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRegistry : MonoBehaviour {
  // Slots and playerSlotAssignments indexes must be the same length.
  // It is assumed that every element in slots is occupied. However, it's okay
  // if some or all of the slotControllers are not occupied
  [SerializeField] private Slot[] slots = new Slot[2];

  private SlotController[] playerSlotAssignments = new SlotController[2];

  private static SlotRegistry _instance;
  public static SlotRegistry Instance { get { return _instance; }}

  void Awake() {
    if (_instance != null && _instance != this) {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }
  }

  public void GetNewSlot(SlotController slotController) {
    // If the controller is already controlling a slot, we'll look for the next
    // free slot starting above that index. Otherwise, we'll start the search
    // from slot 0
    int playerIndex = Array.IndexOf(playerSlotAssignments, slotController);
    int nextFreeIndex = NextFreeSlotIndex(playerIndex);

    // If there is no free slot, abort.
    if (nextFreeIndex == -1) return;

    RevokeSlot(slotController, playerIndex);
    AssignSlot(slotController, nextFreeIndex);
  }

  private void AssignSlot(SlotController slotController, int slotIndex) {
    Slot slot = slots[slotIndex];
    playerSlotAssignments[slotIndex] = slotController;
    slotController.Connect(slot);
  }

  private void RevokeSlot(SlotController slotController, int slotIndex) {
    if (slotIndex == -1) return;

    Slot slot = slots[slotIndex];
    playerSlotAssignments[slotIndex] = null;
    slotController.Disconnect(slot);
  }

  private int NextFreeSlotIndex(int startIndex = 0) {
    if (startIndex == -1) startIndex = 0;

    // Starts at start index, but wraps around to look at every slot once
    for(int i = startIndex; i < (slots.Length + startIndex); i++) {
      int index = i % slots.Length;

      if (playerSlotAssignments[index] == null) {
        return index;
      }
    }

    return -1;
  }


  // public bool GetNewSlot(SlotController slotController) {
  //   // If the controller is already controlling a slot, we'll look for the next
  //   // free slot starting above that index. Otherwise, we'll start the search
  //   // from slot 0
  //   int slotControllerIndex = Array.IndexOf(slotControllers, slotController);
  //   int nextFreeIndex = NextFreeSlotIndex(slotControllerIndex);

  //   // If there is a free slot, abort.
  //   if (nextFreeIndex == -1) return false;

  //   ReleaseSlot(slotController); // No-op if not necessary
  //   AssignSlot(slotController, nextFreeIndex);

  //   return true;
  // }

  // public void ReleaseSlot(SlotController slotController) {
  //   int slotControllerIndex = Array.IndexOf(slotControllers, slotController);

  //   // If the controller is already linked to a slot, disconnect it
  //   if (slotControllerIndex != -1) {
  //     slotControllers[slotControllerIndex] = null;
  //     slotController.Disconnect(slots[slotControllerIndex]);
  //   }
  // }

  // private void AssignSlot(SlotController slotController, int slotIndex) {
  //   slotControllers[slotIndex] = slotController;
  //   slotController.Connect(slots[slotIndex]);
  // }
}
