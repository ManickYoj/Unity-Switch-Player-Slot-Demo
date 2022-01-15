using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry : MonoBehaviour {
  // Receivers and transmitterAssignments indexes must be the same length.
  // It is assumed that every element in receivers is occupied. However, it's okay
  // if some or all of the transmitters are not occupied
  [SerializeField] private Receiver[] receivers = new Receiver[2];

  private Transmitter[] transmitterAssignments = new Transmitter[2];

  private static Registry _instance;
  public static Registry Instance { get { return _instance; }}

  void Awake() {
    if (_instance != null && _instance != this) {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }
  }

  public static void GetNewReceiver(Transmitter transmitter) {
    // If the controller is already controlling a receiver, we'll look for the next
    // free receiver starting above that index. Otherwise, we'll start the search
    // from receiver 0
    int transmitterIdx = Array.IndexOf(_instance.transmitterAssignments, transmitter);
    int nextFreeIndex = NextFreeRecvIdx(transmitterIdx);

    // If there is no free receiver, abort.
    if (nextFreeIndex == -1) return;

    RevokeReceiver(transmitter, transmitterIdx);
    AssignReceiver(transmitter, nextFreeIndex);
  }

  private static void AssignReceiver(Transmitter transmitter, int recvIdx) {
    Receiver receiver = _instance.receivers[recvIdx];
    _instance.transmitterAssignments[recvIdx] = transmitter;
    transmitter.Connect(receiver);
  }

  private static void RevokeReceiver(Transmitter transmitter, int recvIdx) {
    if (recvIdx == -1) return;

    Receiver receiver = _instance.receivers[recvIdx];
    _instance.transmitterAssignments[recvIdx] = null;
    transmitter.Disconnect(receiver);
  }

  private static int NextFreeRecvIdx(int startIndex = 0) {
    if (startIndex == -1) startIndex = 0;

    // Starts at start index, but wraps around to look at every receiver once
    for(int i = startIndex; i < (_instance.receivers.Length + startIndex); i++) {
      int index = i % _instance.receivers.Length;

      if (_instance.transmitterAssignments[index] == null) {
        return index;
      }
    }

    return -1;
  }
}
