using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PokemonDataManager))]

public class PokemonApiManager : MonoBehaviour
{
    public string DefaultApiRoot = "https://pokeapi.co/api/v2/pokemon/";

    private PokemonDataManager _dataManager;

    public void Start()
    {
        _dataManager = GetComponent<PokemonDataManager>();
    }

    public void RequestData(string pokemonName)
    {
        // Create uri
        var rootUri = new Uri(DefaultApiRoot);
        if (!Uri.TryCreate(rootUri, pokemonName, out var apiUri))
        {
            Debug.LogError("Invalid API URL");
            return;
        }

        StartCoroutine(GetDataFromApi(apiUri));
    }

    IEnumerator GetDataFromApi(Uri apiUri)
    {
        // Send request
        UnityWebRequest request = UnityWebRequest.Get(apiUri);
        yield return request.SendWebRequest();

        // Handle response
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError("Connection error");
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Data processing error: " + request.error);
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("Protocol error: " + request.error);
                yield break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Successfully fetched Pokemon data \n" + request.downloadHandler.text);
                break;
        }

        _dataManager.SetData(request.downloadHandler.text);
    }
}
