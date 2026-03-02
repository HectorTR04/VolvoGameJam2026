using UnityEngine;
using Assets.Scripts.AudioSystem;
public class StartMusic : MonoBehaviour
{
    void Awake()
    {
        SoundManager.PlayMusic(SoundType.Music_GameTheme);
    }
}
