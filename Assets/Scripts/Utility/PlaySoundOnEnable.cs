using UnityEngine;
using System.Collections;

public class PlaySoundOnEnable : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

	private void OnEnable()
    {
        m_AudioSource.Play();
    }
}
