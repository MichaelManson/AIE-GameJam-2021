using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public void BeginTimer(int duration = 60)
    {
        // Stop the old timer
        StopAllCoroutines();
        
        // Start the new timer
        StartCoroutine(TimerCountdown(duration));
    }

    private IEnumerator TimerCountdown(int duration)
    {
        var time = duration;
        
        // Turn on the timer text
        UIManager.Instance.timerText.gameObject.SetActive(true);
        UIManager.Instance.timerText.text = time.ToString();

        // Countdown by 1 every second and update the text until it reaches zero
        while (time > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            time--;
            UIManager.Instance.timerText.text = time.ToString();
        }
        
        TimerEnd();
    }

    private void TimerEnd()
    {
        StopAllCoroutines();

        GameManager.RoundOver();
    }
}
