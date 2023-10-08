using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Zeptomoby.OrbitTools;
   
   public class OrbitToolsDemo:MonoBehaviour
   {
      public string url = "http://celestrak.org/NORAD/elements/gp.php?GROUP=active&FORMAT=tle";
      string filePath;
      bool done = false;
      public GameObject prefab;

      void Start()
      {
         filePath = Application.persistentDataPath + "/active.txt";
      }

   public void UpdatePosition()
   {
      if(done)
      {
         DestroySatellites();
      }

      string[] lines = System.IO.File.ReadAllLines(filePath);

      for (int i = 0; i < lines.Length; i += 3)
      {
         string str1 = lines[i];
         string str2 = lines[i + 1];
         string str3 = lines[i + 2];
         Main(str1, str2, str3, prefab);
      }

      done = true;
   }

      
      static void Main(string str1, string str2, string str3, GameObject prefab)
      {
         // Sample code to test the SGP4 and SDP4 implementation. The test
         // TLEs come from the NORAD document "Space Track Report No. 3".

         var tle1 = new TwoLineElements(str1, str2, str3);

         PrintPosVel(tle1, str1, prefab);

      }

      // //////////////////////////////////////////////////////////////////////////
      //
      // Routine to output position and velocity information of satellite
      // in orbit described by TLE information.
      //
      static void PrintPosVel(TwoLineElements tle, string str1, GameObject prefab)
      {

         Satellite sat = new Satellite(tle);
         List<Eci> coords = new List<Eci>();

         // Calculate position, velocity
         // mpe = "minutes past epoch"
         
         Eci eci = sat.PositionEci(0);

         // Add the coordinate object to the list
         coords.Add(eci);

         // Iterate over each of the ECI position objects pushed onto the
         // coordinate list, above, printing the ECI position information
         // as we go.
         Debug.Log(coords.Count);
         for (int i = 0; i < coords.Count; i++)
         {
            Eci e = coords[i] as Eci;
            Vector3 spawnPosition = new Vector3((float)e.Position.X, (float)e.Position.Y, (float)e.Position.Z) * 1/100f;
            GameObject instance = Instantiate(prefab, spawnPosition, Quaternion.identity);
            str1 = str1.TrimEnd();
            instance.GetComponent<SatelliteInfo>().satName = str1;
         }
      }

      void DestroySatellites()
      {
         SatelliteInfo[] satellites = FindObjectsOfType<SatelliteInfo>();
         foreach (SatelliteInfo satellite in satellites)
         {
            Destroy(satellite.gameObject);
         }
      }

      
   }

