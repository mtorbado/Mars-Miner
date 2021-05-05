using UnityEngine;

public class OpenField : MonoBehaviour {

    public void getCampoLibre(string campoLibre) {
        GameEvents.current.LoadGameData(campoLibre);
    }
}
