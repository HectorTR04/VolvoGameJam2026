using UnityEngine;
using Assets.Scripts.AudioSystem;
public class StartMusic : MonoBehaviour
{
    void Start()
    {
        SoundManager.Play(SoundType.Music_GameTheme, 1f);
    }
}
