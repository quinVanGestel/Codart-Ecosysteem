using UnityEngine;

public interface IPlayShortSounds
{
    void PlayPop();
}

public class SoundManager : MonoBehaviour, IPlayShortSounds
{
    public AudioSource pop;

    public void PlayPop()
    {
        if (pop != null)
        {
            pop.Play();
        }
    }
}