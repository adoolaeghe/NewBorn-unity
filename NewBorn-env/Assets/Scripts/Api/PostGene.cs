using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostGene : MonoBehaviour {
    public string response;
	// Use this for initialization

    public IEnumerator requestPart(Mutation mutation, string uuid) {
		string url = "https://pnk98uo8jf.execute-api.eu-west-2.amazonaws.com/prod/" + uuid + "/part";

        string jsonStringTrial = JsonUtility.ToJson(mutation).ToString();

        UnityWebRequest www = UnityWebRequest.Put(url, jsonStringTrial);

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
            response = www.downloadHandler.text;
		}
		else
		{
			response = www.downloadHandler.text;

		}
    }



	public IEnumerator requestAgent(Gene gene)
	{
		string url = "https://pnk98uo8jf.execute-api.eu-west-2.amazonaws.com/prod/agent";

        UnityWebRequest www = UnityWebRequest.Post(url, "");
        yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
            Debug.Log("error");
		}
		else
		{
			string uuid = www.downloadHandler.text.Trim('"');
			url = "https://pnk98uo8jf.execute-api.eu-west-2.amazonaws.com/prod/" + uuid + "/part";

			foreach (var mutation in gene.mutations[0])
			{
                mutation.uuid = uuid;
				string jsonStringTrial = JsonUtility.ToJson(mutation).ToString();
				www = UnityWebRequest.Put(url, jsonStringTrial);
				yield return www.SendWebRequest();
			}
		}



	}
}
