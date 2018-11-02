using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostGene : MonoBehaviour {
    public string response;


    public IEnumerator postCell(string cellInfos)
	{
		string url = "https://pnk98uo8jf.execute-api.eu-west-2.amazonaws.com/prod/cell";

        UnityWebRequest www = UnityWebRequest.Post(url, cellInfos);

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
                    transform.gameObject.GetComponent<Gene>().CellInfos.Add(val);
                }
    
                //transform.gameObject.GetComponent<Gene>().CellInfos;
                transform.gameObject.GetComponent<Gene>().isRequestDone = true;
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
