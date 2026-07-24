using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXLibrary", menuName = "Libraries/SFXLibrary")]
public class SFXLibrary : ScriptableObject
{
    public List<SoundEffect> SoundEffects;
}
