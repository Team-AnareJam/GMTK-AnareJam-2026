using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void StartTransition(string name)
    {
        SceneTransitionManager.Instance.StartTransition(name);
    }
}
