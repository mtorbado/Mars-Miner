using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows the robot to pick ores
/// </summary>
public class OrePicker : MonoBehaviour {

    private ILevel level;

    private void Start() {
        level =  gameObject.GetComponent<ILevel>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ore")) {
            Destroy(collision.gameObject);
            level.pickOre();
        }
    }
}
