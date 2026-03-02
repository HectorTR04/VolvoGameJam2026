using UnityEngine;
using Assets.Scripts.AudioSystem;
public class StartMusic : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlayMusic(SoundType.Music_GameTheme);
    }
}
