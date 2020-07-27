using UnityEngine;
using System; 

/// <summary>
/// Allows the robot to pick ores
/// </summary>
public class OrePicker : MonoBehaviour {

    private ILevel level;

    private void Start() {
        level =  gameObject.GetComponent<ILevel>();
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("collided with something");
        if (collider.gameObject.CompareTag("Ore")) {
            Destroy(collider.gameObject);
            GameEvents.current.PickOreTriggerEnter();
        }
        else {
            
        }
    }
}