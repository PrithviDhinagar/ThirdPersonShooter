using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAreasUI : MonoBehaviour
{
    [System.Serializable]
    public class MapAreaImage 
    {
        public Image uiImage;
        public MapArea mapArea;
    }

    [SerializeField] private List<MapAreaImage> mapAreaImageList;

    private void Start()
    {
        foreach(MapAreaImage mapAreaImage in mapAreaImageList)
        {
            mapAreaImage.mapArea.OnCaptured += (object sender, EventArgs e) =>
            {
                mapAreaImage.uiImage.color = Color.green;
            };
        }
    }

}
