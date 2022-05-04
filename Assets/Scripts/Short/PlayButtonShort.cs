using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonShort : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    public void ActivatePlayButton()
    {
        _playButton.enabled = true;
    }
    public void DeactivatePlayButton()
    {
        _playButton.enabled = false;
    }
}
