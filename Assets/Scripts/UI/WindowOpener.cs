using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowOpener : MonoBehaviour
{
    public GameObject inventory;
    public Slider loadingSlider;
    public GameObject loadingPanel;
    public static bool isWindowOpen = false;

    private void Start()
    {
        loadingPanel.SetActive(true);
        inventory.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
            inventory.SetActive(!inventory.activeInHierarchy);

        if(inventory != null)
            isWindowOpen = inventory.activeInHierarchy;

        if (loadingSlider.value >= loadingSlider.maxValue)
            loadingPanel.SetActive(false);
    }
}
