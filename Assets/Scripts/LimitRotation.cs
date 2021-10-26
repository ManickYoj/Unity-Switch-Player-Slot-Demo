using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRotation : MonoBehaviour {

  public Transform[] targets;
  private Vector3[] initialAngles;

  public Vector3 lowerLimits = Vector3.zero; // Must be -180 or greater on the Y axis, -90 or greater on X and Z
  public Vector3 upperLimits = Vector3.zero; // Must be 180 or less, 90 or less on X and Z

  public bool limitX = false;
  public bool limitY = false;
  public bool limitZ = false;

  void Start() {
    initialAngles = new Vector3[targets.Length];

    // Take note of the initial angle so that we can calculate limits
    // based on it.
    for (int i = 0; i < targets.Length; i++) {
      initialAngles[i] = targets[i].localEulerAngles;
    }
  }

  void LateUpdate() {
    for (int i = 0; i < targets.Length; i++) {
      Transform target = targets[i];
      Vector3 initialAngle = initialAngles[i];

      // We use LateUpdate so that anything that's modified the angle
      // has already run
      Vector3 currentAngle = target.localEulerAngles;


      Vector3 desiredDelta = currentAngle - initialAngle;
      // Sometimes, just to keep us on our toes, Unity will throw out
      // the local angle as eg. 359 instead of -1. I don't know if it
      // does the negative version but ya never know.
      if (desiredDelta.x > 180) desiredDelta.x -= 360f;
      if (desiredDelta.x < -180) desiredDelta.x += 360f;
      if (desiredDelta.y > 180) desiredDelta.y -= 360f;
      if (desiredDelta.y < -180) desiredDelta.y += 360f;
      if (desiredDelta.z > 180) desiredDelta.z -= 360f;
      if (desiredDelta.z < -180) desiredDelta.z += 360f;

      // Clamp the delta to the limits
      desiredDelta = Vector3.Max(desiredDelta, lowerLimits);
      desiredDelta = Vector3.Min(desiredDelta, upperLimits);

      // Calculate the desired angle
      Vector3 desiredAngles = initialAngle + desiredDelta;

      // If the script should ignore an axis, then use the original angle
      // instead of the limited angle. If we are not ignoring the axis
      // wrap it into the -180 to 180 degree limit that Unity requires,
      // but only respects itself some of the time
      if (limitX) {
        if (desiredAngles.x > 180) desiredAngles.x -= 360f;
        if (desiredAngles.x < -180) desiredAngles.x += 360f;
      } else desiredAngles.x = currentAngle.x;

      if (limitY) {
        if (desiredAngles.y > 180) desiredAngles.y -= 360f;
        if (desiredAngles.y < -180) desiredAngles.y += 360f;
      } else desiredAngles.y = currentAngle.y;

      if (limitZ) {
        if (desiredAngles.z > 180) desiredAngles.z -= 360f;
        if (desiredAngles.z < -180) desiredAngles.z += 360f;
      } else desiredAngles.z = currentAngle.z;

      // Apply the rotation
      // this.transform.rotation = Quaternion.Euler(desiredAngles);
      target.localEulerAngles = desiredAngles;
    }
  }
}
