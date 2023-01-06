using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingPanelUI : MonoBehaviour
{
    [SerializeField] private GameConfig config;
    
    [SerializeField] private TextMeshProUGUI widthText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI addMaxCountText;
    [SerializeField] private TextMeshProUGUI blockCountText;

    
    
    private void Update()
    {
        widthText.text = config.GridSize.x.ToString();
        heightText.text = config.GridSize.y.ToString();
        addMaxCountText.text = config.AddMaxCount.ToString();
        blockCountText.text = config.BlockCount.ToString();
    }
}
