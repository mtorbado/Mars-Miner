using UnityEngine;

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
        DropCable();
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

    public void DropCable() { 
        animation.PlayQueued("dropCableClip");
    }

    public void RetrieveCable() {
        animation.PlayQueued("retrieveCableClip");
    }

    private float HeithUpdater() {
        return 16 - (Input.mousePosition.y/250);
    }
}
