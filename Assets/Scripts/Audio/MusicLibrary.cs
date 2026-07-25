using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicLibrary", menuName = "Libraries/MusicLibrary")]
public class MusicLibrary : ScriptableObject
{
    public List<MusicClip> MusicClips;
}
