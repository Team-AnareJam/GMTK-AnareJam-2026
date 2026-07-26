using UnityEngine;

public class CloseSettings : MonoBehaviour
{
    public GameObject SettingsPrefab;

    public void ExitSettings()
    {
        Time.timeScale = 1;
        Destroy(SettingsPrefab);
    }
}
