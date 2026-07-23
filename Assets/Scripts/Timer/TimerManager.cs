using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Variables
    public static TimerManager Instance;
    //public static event Action OnStartTimer;
    //public static event Action OnPauseTimer;
    //public static event Action OnUpdateTimer;

    [SerializeField] private float timeRemaining;
    public float TimeRemaining
    {
        get => timeRemaining;
        set
        {
            if (timeRemaining != value)
            {
                if (value > MaxTime)
                {
                    float overtime = (value) - MaxTime;
                    float overtimeDecimals = overtime - Mathf.Floor(overtime);
                    timeRemaining = MaxTime + overtimeDecimals;
                }
                else
                {
                    timeRemaining = value;
                }
            }
        }
    }
    public float MaxTime;
    public bool TimerHasStarted;
    public bool TimerIsActive;
    public float TimerSpeed = 1;
    public bool IndicatorIsActive;

    public TMP_Text TimerMinute;
    public TMP_Text TimerSecond;
    public Image TimerBar;
    public Image TimerBarBG;
    public Image TimerBarIndicator;
    public float IndicatorMaxDuration;
    public float IndicatorCurrentDuration;

    [SerializeField] private Material textMaterial;
    [SerializeField] private Gradient barGradient;
    [SerializeField] private Gradient textGradient;
    [SerializeField] private Gradient activeIndicatorGradient;

    [SerializeField] private Gradient timeUpGradient;
    [SerializeField] private Gradient timeDownGradient;
    #endregion

    #region Monobehaviour Functions
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    private void OnEnable()
    {
        GameManager.OnStartGame += StartTimer;
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IndicatorIsActive) UpdateTimerChangeUI();
        
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
    #endregion

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

    [SerializeField] private int addedSeconds = 5;
    [Button]
    public void UpdateTimer()
    {
        UpdateTimer(addedSeconds);
    }
    public void UpdateTimer(int seconds)
    {
        TimeRemaining += seconds;
        IndicatorIsActive = true;

        if (seconds >= 0) activeIndicatorGradient = timeUpGradient;        
        else activeIndicatorGradient = timeDownGradient;
        IndicatorCurrentDuration = IndicatorMaxDuration;
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

    public void UpdateTimerChangeUI()
    {
        IndicatorCurrentDuration -= Time.deltaTime;
        TimerBarIndicator.color = activeIndicatorGradient.Evaluate(IndicatorCurrentDuration);
        if (IndicatorCurrentDuration < 0) IndicatorIsActive = false;
    }
    #endregion
}
