using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static bool _countdown;
    
    public async void BeginTimer(int duration = 60)
    {
        // Stop the old timer
        StopAllCoroutines();

        _countdown = true;
        
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
            if (!_countdown) continue;
            
            yield return new WaitForSecondsRealtime(1f);
            time--;
            UIManager.Instance.timerText.text = time.ToString();
        }
        
        TimerEnd();
    }

    public static void PauseTimer() => _countdown = false;
    public static void ResumeTimer() => _countdown = true;
    
    private void TimerEnd()
    {
        StopAllCoroutines();

        GameManager.RoundOver();
    }
}
