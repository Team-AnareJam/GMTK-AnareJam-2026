using System.Collections;
using UnityEngine;

public class BootToMenu : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartTransition());
    }

    public IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(5);
        SceneTransitionManager.Instance.StartTransition("Menu");
    }
}
