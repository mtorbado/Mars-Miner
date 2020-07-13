using UnityEngine;

/// <summary>
/// Class to update the position of the magnet cable when it's deployed and call its different animations
/// </summary>
public class CableController : MonoBehaviour {
    public AnimationClip dropCableClip;
    public AnimationClip retrieveCableClip;

    new Animation animation;
    BoardManager boardManager;
    Ray mouseRay;
    Plane basePlane;

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        basePlane = boardManager.GetBasePlane();
        AnimationSetup();
    }

    private void Start() {
        DropCable(); //TODO: move to an UI button
    }

    private void Update() {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (basePlane.Raycast(mouseRay, out float distance)) {
            this.transform.position = new Vector3(mouseRay.GetPoint(distance).x, HeithUpdater(), mouseRay.GetPoint(distance).z);
        }
    }

    private void AnimationSetup() {
        animation = this.GetComponentInChildren<Animation>();
        animation.AddClip(dropCableClip, "dropCableClip");
        animation.AddClip(retrieveCableClip, "retrieveCableClip");
    }

    /// <summary>
    /// Plays the drop cable animation
    /// </summary>
    public void DropCable() { 
        animation.PlayQueued("dropCableClip");
    }

    /// <summary>
    /// Plays the retrieve cable animation
    /// </summary>
    public void RetrieveCable() {
        animation.PlayQueued("retrieveCableClip");
    }

    /// <summary>
    /// Function to recalculate the cable heith depending on it's position, to improve the visibility of the board
    /// This function is calibrated acording to the camera position and angle of view, be careful if camera position is modified
    /// </summary>
    /// <returns></returns>
    private float HeithUpdater() {
        return 16 - (Input.mousePosition.y/250);
    }
}
