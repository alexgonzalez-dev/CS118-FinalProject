using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int sceneIndex;
    public string rigTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(rigTag) || other.transform.root.CompareTag(rigTag))
            SceneManager.LoadScene(sceneIndex);
    }
}
