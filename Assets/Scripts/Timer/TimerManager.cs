using NaughtyAttributes;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public static event Action OnStartTimer;
    public static event Action OnPauseTimer;
    public static event Action OnUpdateTimer;

    public float TimeRemaining;
    public float MaxTime;
    public bool TimerHasStarted;
    public bool TimerIsActive;
    public float TimerSpeed;

    public TMP_Text TimerMinute;
    public TMP_Text TimerSecond;
    public Image TimerBar;
    public Image TimerBarBG;

    [SerializeField] private Material textMaterial;
    [SerializeField] private Gradient barGradient;
    [SerializeField] private Gradient textGradient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimerIsActive) return;
        
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime * TimerSpeed;
            UpdateTimerUI();
        }

        if (TimeRemaining <= 0)
        {
            //TODO: IF CARDS ARE ACTIVE, MAINTAIN GRACE PERIOD
            //TODO: IF NO CARDS ACTIVE (ANYMORE), INITIATE GAME END.
        }
    }

    #region Internal Timer Management
    public void StartTimer()
    {
        if (!TimerHasStarted)
        {
            TimeRemaining = MaxTime;
            TimerHasStarted = true;
        }

        TimerIsActive = true;
        UpdateTimerUI();
    }

    public void PauseTimer()
    {
        TimerIsActive = false;
        UpdateTimerUI();
    }

    public void UpdateTimer(int seconds)
    {
        TimeRemaining += seconds;
        // TODO: trigger UI popup based on sign of seconds variable;
    }
    #endregion

    #region Timer UI
    [Button]
    public void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(TimeRemaining / 60);
        int seconds = Mathf.Clamp(Mathf.FloorToInt(TimeRemaining % 60), 0, 59);
        
        TimerMinute.text = Mathf.Clamp(minutes, 0, 100).ToString("00");
        TimerSecond.text = seconds.ToString("00");

        float time = MathAE.RemapFloat(TimeRemaining, 0, MaxTime, 0, 1);

        float width = (TimeRemaining / MaxTime);
        TimerBar.fillAmount = Mathf.Clamp01(width);
        TimerBarBG.color = barGradient.Evaluate(time);
        textMaterial.SetColor("_UnderlayColor", textGradient.Evaluate(time));
    }
    #endregion
}
