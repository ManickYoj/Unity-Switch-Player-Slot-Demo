using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Receiver : MonoBehaviour {
  public Camera receiverCamera;
  public GameObject[] listeners;

  public void SendCommand(string cmd, object val = null) {
    foreach (GameObject listener in listeners) {
      listener.SendMessage(cmd, val, SendMessageOptions.DontRequireReceiver);
    };
  }
}