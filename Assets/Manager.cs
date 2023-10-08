using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class Manager : MonoBehaviour
{
    string url = "http://celestrak.org/NORAD/elements/gp.php?GROUP=active&FORMAT=tle";
    public string filePath;

    public GameObject SatellitePositioner;
    public TMP_InputField inputField;
    public TMP_Text text;

    public TMP_InputField radiusInputField;
    public TMP_Text nSatelliteText;

    public TMP_Text satNameText;
    public TMP_Text satCoordsText;

    public TMP_Text satVelocityText;

    public TMP_Text satAltitudeText;

    public GameObject Camera;

    GameObject currentSatellite;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/active.txt";
        OnCLick();
    }

    // Update is called once per frame
    void Update()
    {
        //updateSatelliteInfo();
    }

    public void OnCLick()
    {
        // Check if file exists and delete it if it does
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        //Donwload data from url and save it to a file
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(url);
        StreamReader reader = new StreamReader(stream);
        string content = reader.ReadToEnd();
        reader.Close();
        stream.Close();
        File.Create(filePath).Close();
        File.WriteAllText(filePath, content);
        SatellitePositioner.GetComponent<OrbitToolsDemo>().UpdatePosition();
    }

    public void OnClick2()
    {
        SatelliteInfo[] satellites = FindObjectsOfType<SatelliteInfo>();
        if(!string.IsNullOrEmpty(inputField.text))
        {
            foreach(SatelliteInfo satellite in satellites)
            {
                Debug.Log(inputField.text.Length);
                if(satellite.satName == inputField.text)
                {
                    Debug.Log("Found satellite");
                    if(currentSatellite != null)
                    {
                        currentSatellite.GetComponent<SatelliteInfo>().isCurrentSatellite = false;
                    }
                    Camera.GetComponent<Control>().target = satellite.gameObject.transform;
                    Camera.GetComponent<Control>().Update();
                    currentSatellite = satellite.gameObject;
                    currentSatellite.GetComponent<SatelliteInfo>().isCurrentSatellite = true;
                    updateSatelliteInfo();
                    return;
                }
            }

            text.text = "Satellite not found";
        }
    }

    public void onRadiusChanged()
    {
        if(currentSatellite != null)
        {
            SatelliteInfo[] satellites = FindObjectsOfType<SatelliteInfo>();
            foreach(SatelliteInfo satellite in satellites)
            {
                satellite.isInRadius = false;
            }
            Vector3 satellitePosition = currentSatellite.transform.position;
            int satCount = -1;
            foreach(SatelliteInfo satellite in satellites)
            {
                Vector3 newSat = satellite.gameObject.transform.position;
                float distance = Vector3.Distance(satellitePosition, newSat);
                if(float.Parse(radiusInputField.text)/100f >= distance)
                {
                    satellite.isInRadius = true;
                    satCount++;
                }
            }
            nSatelliteText.text = satCount.ToString();
        }
    }

    public void updateSatelliteInfo()
    {
        if(currentSatellite != null)
        {
            satNameText.text = currentSatellite.GetComponent<SatelliteInfo>().satName;

            float x = currentSatellite.transform.position.x * 100f;
            float y = currentSatellite.transform.position.y * 100f;
            float z = currentSatellite.transform.position.z * 100f;
            string coords = x.ToString() + ", " + y.ToString() + ", " + z.ToString();
            satCoordsText.text = coords;

            float altitude = (Vector3.Distance(currentSatellite.transform.position, Vector3.zero) - 63.71f) * 100f;
            satAltitudeText.text = altitude.ToString();

        }
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
