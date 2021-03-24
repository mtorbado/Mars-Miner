using System.Collections;

/// <summary>
/// Game level interface
/// </summary>
public interface ILevel {
    IEnumerator Play();
    void PickOre();
}