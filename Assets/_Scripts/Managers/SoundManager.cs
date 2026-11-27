using UnityEngine;

public interface IPlayShortSound
{
    void PlayPop();
}

public class SoundManager : MonoBehaviour, IPlayShortSound
{
    [Header("Sounds")]
    public AudioSource pop;

    public void PlayPop()
    {
        if (pop == null)
        {
            Debug.LogWarning("Pop AudioSource is missing!");
            return;
        }

        pop.PlayOneShot(pop.clip);
    }
}