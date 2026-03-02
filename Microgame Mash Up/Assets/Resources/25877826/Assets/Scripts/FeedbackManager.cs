using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour
{
  [Header("Audio Sources")]
  public AudioSource bgMusicSource;
  public AudioSource crowdSource;
  public AudioSource sfxSource;

  [Header("Audio Clips")]
  public AudioClip successClip;
  public AudioClip failClip;
  public AudioClip winGameClip;
  public AudioClip loseGameClip;

  [Header("Visuals")]
  public Image flashImage;
  public float flashDuration = 0.2f;

  void Start()
  {
    bgMusicSource.loop = true;
    bgMusicSource.volume = 0.15f;
    bgMusicSource.Play();

    crowdSource.loop = true;
    crowdSource.volume = 0.3f;
    crowdSource.Play();

    if (flashImage != null) flashImage.color = new Color(1, 1, 1, 0);
  }

  public void PlaySuccess()
  {
    sfxSource.PlayOneShot(successClip);
    StartCoroutine(FlashScreen(new Color(0f, 1f, 0f, 0.4f)));
  }

  public void PlayFail()
  {
    sfxSource.PlayOneShot(failClip);
    StartCoroutine(FlashScreen(new Color(1f, 0f, 0f, 0.4f)));
  }

  public void PlayWinGame() => sfxSource.PlayOneShot(winGameClip);
  public void PlayLoseGame() => sfxSource.PlayOneShot(loseGameClip);

  private IEnumerator FlashScreen(Color targetColor)
  {
    if (flashImage == null) yield break;
    flashImage.color = targetColor;

    float time = 0;
    while (time < flashDuration)
    {
      time += Time.deltaTime;
      float alpha = Mathf.Lerp(targetColor.a, 0f, time / flashDuration);
      flashImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
      yield return null;
    }

    flashImage.color = new Color(1, 1, 1, 0);
  }
}