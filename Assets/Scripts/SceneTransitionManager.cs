using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] private GameData gameData;
    [SerializeField] private Material backgroundMat;
    private WaveManager waveManager;

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
    public SpriteRenderer SR;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
    }

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
        SR.color = new Color(0, 0, 0, CurrentFade);

        if (FadingIn && CurrentFade >= 1)
        {
            FadingIn = false;
            CurrentFade = 1;
            TransitionToScene();
        }
        waveManager = (WaveManager)GameObject.FindAnyObjectByType(typeof(WaveManager));
        if(gameData != null  && backgroundMat != null)
        {
            if (waveManager != null)
            {
                backgroundMat.SetTexture("_background", gameData.Levels[gameData.CurrentLevel].BackgroundTexture);
                waveManager.Waves = gameData.Levels[gameData.CurrentLevel].Waves;
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
