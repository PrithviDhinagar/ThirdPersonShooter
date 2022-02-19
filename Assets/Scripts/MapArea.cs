using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    public event EventHandler OnCaptured;
    public event EventHandler OnPlayerEnter;
    public event EventHandler OnPlayerExit;
    public enum State
    {
        Neutral,
        Captured,
    }
    private List<MapAreaCollider> mapAreaColliderList;
    private State state;
    private float progress;
    private void Awake()
    {
        mapAreaColliderList = new List<MapAreaCollider>();

        foreach(Transform child in transform)
        {
            MapAreaCollider mapAreaCollider = child.GetComponent<MapAreaCollider>();
            if(mapAreaCollider != null)
            {
                mapAreaColliderList.Add(mapAreaCollider);
                mapAreaCollider.OnPlayerEnter += MapAreaCollider_OnPlayerEnter;
                mapAreaCollider.OnPlayerExit += MapAreaCollider_OnPlayerExit;

            }
        }

        state = State.Neutral;
    }

    private void MapAreaCollider_OnPlayerExit(object sender, EventArgs e)
    {
        bool hasPlayerInside = false;
        foreach (MapAreaCollider mapAreaCollider in mapAreaColliderList)
        {
            if(mapAreaCollider.GetPlayerMapAreasList().Count>0)
            {
                hasPlayerInside = true;
            }
        }

        if(!hasPlayerInside)
        {
            OnPlayerExit?.Invoke(this, EventArgs.Empty);
        }
    }

    private void MapAreaCollider_OnPlayerEnter(object sender, EventArgs e)
    {
        OnPlayerEnter?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Neutral:
                List<PlayerMapAreas> playerMapAreasInsideList = new List<PlayerMapAreas>();

                foreach (MapAreaCollider mapAreaCollider in mapAreaColliderList)
                {
                    foreach (PlayerMapAreas playerMapAreas in mapAreaCollider.GetPlayerMapAreasList())
                    {
                        if (!playerMapAreasInsideList.Contains(playerMapAreas))
                        {
                            playerMapAreasInsideList.Add(playerMapAreas);
                        }
                    }
                }

                float progressSpeed = 0.5f;
                progress += playerMapAreasInsideList.Count * progressSpeed * Time.deltaTime;

                Debug.Log("playerCountInsideMapArea: " + playerMapAreasInsideList + "; progress: " + progress);

                if (progress >= 1f)
                {
                    state = State.Captured;
                    OnCaptured?.Invoke(this, EventArgs.Empty);
                    Debug.Log("Captured");
                }
                break;
            case State.Captured:
                break;
        }
    }

    public float GetProgress()
    {
        return progress;
    }
}
