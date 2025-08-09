using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ComicPanel : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] private Image[] comicPanel;
    [SerializeField] private int imageIndex;
    [SerializeField] GameObject playButton;

    private Image myImage;
    private bool comicShowOver;

    private void Start()
    {
        myImage = GetComponent<Image>();
        ShowNextImage();
    }

    private void ShowNextImage()
    {
        if (comicShowOver)
            return;

        StartCoroutine(ChangeImageAlpha(1, 1.5f, ShowNextImage));
    }

    private IEnumerator ChangeImageAlpha(float targetAlpha,float duration,System.Action onComplete)
    {
        float time = 0;
        Color currentColor = comicPanel[imageIndex].color;
        float startAlpha = currentColor.a;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            comicPanel[imageIndex].color = new Color(currentColor.r,currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        comicPanel[imageIndex].color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

        imageIndex++;

        if(imageIndex >= comicPanel.Length)
        {
            EnablePlayButton();
        }

        onComplete?.Invoke();
    }

    private void EnablePlayButton()
    {
        StopAllCoroutines();
        playButton.SetActive(true);
        comicShowOver = true;
        myImage.raycastTarget = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShowNextImageOnClick();
    }

    private void ShowNextImageOnClick()
    {
        comicPanel[imageIndex].color = Color.white;
        imageIndex++;

        if (imageIndex >= comicPanel.Length)
            EnablePlayButton();

        if (comicShowOver)
            return;

        ShowNextImage();
    }
}
