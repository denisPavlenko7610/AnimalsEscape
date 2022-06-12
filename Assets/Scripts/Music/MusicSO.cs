using System.Collections.Generic;
using UnityEngine;

namespace AnimalsEscape.Music
{
    [CreateAssetMenu(fileName = "MusicConfig", menuName = "Music/Create config", order = 0)]
    public class MusicSO : ScriptableObject
    {
        public List<AudioClip> Music = new();
        public List<AudioClip> MusicFX = new();
    }
}