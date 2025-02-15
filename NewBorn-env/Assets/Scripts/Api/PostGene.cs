﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Gene;

public class PostGene : MonoBehaviour {
    public string response;


    public IEnumerator postCell(string cellInfos, string cellName)
	{
		string url = "https://pnk98uo8jf.execute-api.eu-west-2.amazonaws.com/prod/cell";
        PostObject postObject = new PostObject(cellInfos, cellName);

        string jsonStringTrial = JsonUtility.ToJson(postObject);
    
        UnityWebRequest www = UnityWebRequest.Put(url, jsonStringTrial);
      

        yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
            Debug.Log("error");
		}
	}

    public IEnumerator getCell(string id)
    {
      
        using (UnityWebRequest www = UnityWebRequest.Get("https://pnk98uo8jf.execute-api.eu-west-2.amazonaws.com/prod/cell/" + id))
        {
            yield return www.Send();

            if(www.isDone) {
                string[] textResponse = www.downloadHandler.text.Split('A');
                for (int i = 1; i < textResponse.Length; i++)
                {
                    float val = float.Parse(textResponse[i].Split('"')[0], System.Globalization.CultureInfo.InvariantCulture);
                    transform.gameObject.GetComponent<Cell>().CellInfos.Add(val);
                }
    
                //transform.gameObject.GetComponent<Gene>().CellInfos;
                transform.gameObject.GetComponent<Cell>().isRequestDone = true;
            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}

public class PostObject {
    public string cellInfos;
    public string cellName;

    public PostObject(string cellInfos, string cellName) {
        this.cellInfos = cellInfos;
        this.cellName = cellName; 
    }
}
