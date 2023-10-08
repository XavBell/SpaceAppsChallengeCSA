using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SatelliteInfo : MonoBehaviour
{
    public string satName;
    public TMP_Text satNameText;
    public Material satMaterial;
    public Material satRadiusMaterial;
    public Material satCurrentMaterial;
    public Material material;
    
    public GameObject cube;

    public bool isInRadius = false;
    public bool isCurrentSatellite = false;

    void Start()
    {
        satNameText.text = satName;
    }

    void Update()
    {
        if (isInRadius && isCurrentSatellite == false)
        {
            cube.GetComponent<MeshRenderer>().material = satRadiusMaterial;
            return;
        }
        else if(isInRadius == false && isCurrentSatellite == false)
        {
            cube.GetComponent<MeshRenderer>().material = satMaterial;
        }

        if (isCurrentSatellite)
        {
            cube.GetComponent<MeshRenderer>().material = satCurrentMaterial;
            return;
        }
        else
        {
            cube.GetComponent<MeshRenderer>().material = satMaterial;
        }
    }
}
