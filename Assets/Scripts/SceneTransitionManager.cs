using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    public GameData GameData;
    [SerializeField] private Material backgroundMat;
    private WaveManager waveManager;

    public bool ReloadScene = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    public static event Action OnTransitionFinished;
    
    public bool TransitionHasStarted;
    public string SceneToTransitionTo = "";
    public bool FadingIn = true;
    public float CurrentFade = 0;
    public float FadeSpeed = 1f;
    public Image FadeImage;

    // Update is called once per frame
    void Update()
    {
        if (!TransitionHasStarted) return;
        Fade();
    }

    public void StartTransition(string name)
    {
        SceneToTransitionTo = name;
        TransitionHasStarted = true;
    }

    public void Fade()
    {
        CurrentFade += (FadingIn ? FadeSpeed : -FadeSpeed) * Time.deltaTime;
        FadeImage.color = new Color(0, 0, 0, CurrentFade);

        if (FadingIn && CurrentFade >= 1)
        {
            FadingIn = false;
            CurrentFade = 1;
            TransitionToScene();
        }
        waveManager = (WaveManager)GameObject.FindAnyObjectByType(typeof(WaveManager));
        if(GameData != null  && backgroundMat != null)
        {
            if (waveManager != null)
            {
                backgroundMat.SetTexture("_background", GameData.Levels[GameData.CurrentLevel].BackgroundTexture);
                waveManager.Waves = GameData.Levels[GameData.CurrentLevel].Waves;
            }
        }
        if (!FadingIn && CurrentFade <= 0)
        {
            CurrentFade = 0;
            TransitionHasStarted = false;
            FadingIn = true;
            EndTransition();
        }
    }

    public void TransitionToScene()
    {
        SceneManager.LoadScene(SceneToTransitionTo);
    }

    public void EndTransition()
    {
        SceneToTransitionTo = "";
        OnTransitionFinished?.Invoke();
    }
}
