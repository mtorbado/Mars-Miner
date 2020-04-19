using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableController : MonoBehaviour
{
    public AnimationClip dropCableClip;
    public AnimationClip retrieveCableClip;

    Animation animation;
    BoardManager boardManager;
    Ray mouseRay;
    Plane basePlane;

    private void Awake()
    {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        basePlane = boardManager.GetBasePlane();
        AnimationSetup();
        
    }

    private void Start()
    {
        DropCable();
    }

    private void Update()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (basePlane.Raycast(mouseRay, out float distance))
        {
            this.transform.position = new Vector3(mouseRay.GetPoint(distance).x, 14, mouseRay.GetPoint(distance).z);
        }
    }

    public void DropCable()
    {
        animation.PlayQueued("dropCableClip");
    }

    public void RetrieveCable()
    {
        animation.PlayQueued("retrieveCableClip");
    }

    private void AnimationSetup()
    {
        animation = this.GetComponentInChildren<Animation>();
        animation.AddClip(dropCableClip, "dropCableClip");
        animation.AddClip(retrieveCableClip, "retrieveCableClip");
    }
}
