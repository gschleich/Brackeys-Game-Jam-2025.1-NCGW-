using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundPlayer : MonoBehaviour
{
    public void PlayClickSound()
    {
        SoundManager.Instance.PlaySound2D("GUIBuy");
    }

    public void PlayRangeClickSound()
    {
        SoundManager.Instance.PlaySound2D("RangePowerUp");
    }

    public void PlayFireRateClickSound()
    {
        SoundManager.Instance.PlaySound2D("FireRatePowerUp");
    }
}
