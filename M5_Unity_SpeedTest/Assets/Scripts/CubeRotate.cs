using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeRotate : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private string apiUrl;
    [SerializeField] private string aptDirectory;
    private IEnumerator Start()
    {
        while (Application.isPlaying)
        {
            var req = UnityWebRequest.Get(apiUrl + aptDirectory);
            yield return req.SendWebRequest();
            NineDofData nineDofData = JsonUtility.FromJson<NineDofData>(req.downloadHandler.text);
            Vector3 rotate = new Vector3(nineDofData.gyroscope[0], nineDofData.gyroscope[1], nineDofData.gyroscope[2]);
            cube.transform.rotation = Quaternion.Euler(rotate);
            yield return new WaitForEndOfFrame();
        }
    }
}
