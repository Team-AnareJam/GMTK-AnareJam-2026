using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public MusicLibrary MusicLib;
    public bool Intense;
    public bool IntroStarted;

    public MusicClip CurrentClip;
    public string NextClip;
    public int MusicPlayerIndex = 0;
    public AudioSource[] MusicPlayers;

    [SerializeField] private bool playingMusic;

    public double GoalTime = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    private void Start()
    {
        if (!playingMusic) return;
        StartIntroSong();
    }

    private void Update()
    {
        if (AudioSettings.dspTime > GoalTime - 5)
        {
            string nextClip = NextClip + (Intense ? "INTENSE" : "DEFAULT");
            if (!MusicLib.MusicClips.Any(SFX => SFX.Name == nextClip)) return;
            CurrentClip = MusicLib.MusicClips.First(SFX => SFX.Name == nextClip);
            PlayScheduledClip();
        }
    }

    public void StartIntroSong()
    {
        CurrentClip = MusicLib.MusicClips.First(SFX => SFX.Name == "Intro");
        GoalTime = AudioSettings.dspTime + 0.5;
        PlayScheduledClip();
    }

    public void PlayScheduledClip()
    {
        MusicPlayers[MusicPlayerIndex].clip = CurrentClip.Clip;
        MusicPlayers[MusicPlayerIndex].PlayScheduled(GoalTime);

        GoalTime += CurrentClip.SongSampleCount / CurrentClip.Clip.frequency;
        NextClip = CurrentClip.NextClip;
        Debug.Log(NextClip);

        MusicPlayerIndex = 1 - MusicPlayerIndex;
    }
}

[System.Serializable]
public class MusicClip
{
    public string Name;
    public AudioClip Clip;
    public int BPM;
    public int BeatsPerBar;

    public float LoopStartTime;
    public float LoopEndTime;

    public string NextClip;

    public float BeatLength => 60 / (float)BPM;
    public float BarLength => BeatLength * BeatsPerBar;
    public float LoopLength => LoopEndTime - LoopStartTime;
    public float SongLength => Clip.length - BarLength;
    public double SamplesPerBeat => Clip.samples / (Clip.length / BeatLength);
    public double SongSampleCount => Clip.samples - (SamplesPerBeat * BeatsPerBar);
}