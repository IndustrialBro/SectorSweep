using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CamControls : MonoBehaviour
{
    public static CamControls Instance { get; private set; }
    private CamControls() { }
    [SerializeField]
    RawImage camView;
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
        camView.gameObject.SetActive(false);
        SwitchMount("u1");
    }
    public void ShowHideCam()
    {
        camView.gameObject.SetActive(!camView.gameObject.activeSelf);
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
