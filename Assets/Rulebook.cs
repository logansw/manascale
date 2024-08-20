using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rulebook : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> rulebookPages;
    int page;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    void Start()
    {
        page = 0;
        _image.sprite = rulebookPages[page];
        _previousButton.interactable = false;
    }

    public void FlipForwards()
    {
        page++;
        if (page >= rulebookPages.Count)
        {
            page = 0;
        }
        _image.sprite = rulebookPages[page];
        _nextButton.interactable = page < rulebookPages.Count - 1;
        _previousButton.interactable = true;
    }

    public void FlipBackwards()
    {
        page--;
        if (page < 0)
        {
            page = rulebookPages.Count - 1;
        }
        _image.sprite = rulebookPages[page];
        _previousButton.interactable = page > 0;
        _nextButton.interactable = true;
    }
}
