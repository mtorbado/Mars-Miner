using System.Collections;

public interface ILevel {
    IEnumerator Play(string[] args);
    void Initialize();
    void PickOre();
}