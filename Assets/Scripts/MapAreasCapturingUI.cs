using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAreasCapturingUI : MonoBehaviour
{
    [SerializeField] private List<MapArea> mapAreaList;

    private MapArea mapArea;
    private Image progressImage;

    private void Awake()
    {
        progressImage = transform.Find("ProgressImage").GetComponent<Image>();
    }

    private void Start()
    {
        foreach(MapArea mapArea in mapAreaList)
        {
            mapArea.OnPlayerEnter += MapArea_OnPlayerEnter;
            mapArea.OnPlayerExit += MapArea_OnPlayerExit;
        }

        Hide();
    }

    private void Update()
    {
        progressImage.fillAmount = mapArea.GetProgress();
    }

    private void MapArea_OnPlayerExit(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void MapArea_OnPlayerEnter(object sender, System.EventArgs e)
    {
        mapArea = sender as MapArea;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
