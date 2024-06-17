using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject[] pages;
    public TMP_Text pageIndicator;
    private int currentPageIndex = 0;

    private void Start()
    {
        // Zorg ervoor dat alle pagina's gedeactiveerd zijn behalve de eerste
        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        if (pages.Length > 0)
        {
            pages[0].SetActive(true);
        }
        pageIndicator.text = $"page 1/{pages.Length}";
    }

    public void NextPage()
    {
        if (pages.Length == 0) return;

        // Deactiveer de huidige pagina
        pages[currentPageIndex].SetActive(false);

        // Ga naar de volgende pagina (met looping)
        currentPageIndex = (currentPageIndex + 1) % pages.Length;

        // Activeer de nieuwe pagina
        pages[currentPageIndex].SetActive(true);
        pageIndicator.text = $"page {currentPageIndex + 1}/{pages.Length}";
    }
}
