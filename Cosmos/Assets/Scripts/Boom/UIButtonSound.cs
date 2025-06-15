using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour, IPointerClickHandler, ISelectHandler, IPointerEnterHandler
{
    public AudioClip clickSound;
    public AudioClip hoverSound;

    private AudioSource audioSource;
    private bool hoverPlayed = false;

    void Start()
    {
        audioSource = GameObject.Find("UIAudioPlayer")?.GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(PlayClick);
    }
    void Awake()
    {
        audioSource = GameObject.Find("UIAudioPlayer")?.GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHover();
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlayHover();
    }

    private void PlayClick()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    private void PlayHover()
    {
        if (audioSource != null && hoverSound != null && !hoverPlayed)
        {
            audioSource.PlayOneShot(hoverSound);
            hoverPlayed = true;

            Invoke(nameof(ResetHover), 0.1f);
        }
    }

    private void ResetHover()
    {
        hoverPlayed = false;
    }
}