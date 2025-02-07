using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CamControls : MonoBehaviour
{
    public static CamControls Instance { get; private set; }
    private CamControls() { }
    [SerializeField]
    RawImage camView, mmView;
    [SerializeField]
    Camera cam;
    List<CameraMount> cameraMounts = new List<CameraMount>();
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        SwitchMount("u1");
    }
    public void ShowHideCam()
    {
        Vector2 tempDim = camView.rectTransform.sizeDelta;
        Vector2 tempLoc = camView.rectTransform.anchoredPosition;
        
        camView.rectTransform.sizeDelta = mmView.rectTransform.sizeDelta;
        camView.rectTransform.anchoredPosition = mmView.rectTransform.anchoredPosition;

        mmView.rectTransform.sizeDelta = tempDim;
        mmView.rectTransform.anchoredPosition = tempLoc;
    }
    public void SwitchMount(string id)
    {
        foreach(CameraMount mount in cameraMounts)
        {
            if(mount.id == id)
            {
                cam.transform.SetParent(mount.transform, false);
            }
        }
    }
    public void AddMount(CameraMount mount)
    {
        cameraMounts.Add(mount);
    }
}
