using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using System.Linq;

public class Pokemon : MonoBehaviour
{
    // Public
    [Header("Input")]
    public TMP_InputField NameInput;

    [Header("Output")]
    public TMP_Text NameText;
    public TMP_Text PokedexNumberText;
    public TMP_Text TypeText;
    public TMP_Text HeightText;
    public TMP_Text WeightText;
    public TMP_Text DescriptionText;
    public TMP_Text BaseStatsText;
    public TMP_Text AbilitiesText;

    // Private
    private const string _apiKey = "https://pokeapi.co/api/v2/pokemon/";

    public void Start()
    {
        StartCoroutine(UpdatePokemonData());
    }

    IEnumerator UpdatePokemonData()
    {
        // Get name & validate
        string pokemonName = NameInput.text.ToLower(); // Assume the user inputs the Pokémon name here.
        if (!pokemonName.All(char.IsLetterOrDigit))
        {
            Debug.LogError("Invalid input");
            yield break;
        }

        // Request from API
        string requestUrl = _apiKey + pokemonName;
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
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
                Debug.Log("Success \n" + request.downloadHandler.text);
                break;
        }

        // Get data & save
        string rawData = request.downloadHandler.text;
        SaveData(rawData);

        // Update UI
        PokemonData data = JsonUtility.FromJson<PokemonData>(rawData);
        UpdateUI(data);
    }

    [System.Serializable]
    public class PokemonData
    {
        public int id;
        public string name;
        public int height;
        public int weight;
        public List<TypeInfo> types;
    }

    // TODO: CLEAN UP TypeInfo + TypeDetail, irrelevant structures
    [System.Serializable]
    public class TypeInfo
    {
        public TypeDetail type;
    }

    [System.Serializable]
    public class TypeDetail
    {
        public string name;
    }

    private void UpdateUI(PokemonData data)
    {
        // Populate the UI with data.
        NameText.text = data.name.ToUpper();
        PokedexNumberText.text = "Pokedex Number: " + data.id.ToString();
        TypeText.text = "Type: " + data.types[0].type.name; // Only taking the first type for demonstration.
        HeightText.text = "Height: " + data.height.ToString();
        WeightText.text = "Weight: " + data.weight.ToString();

        // Assume you have fetched Abilities and Base Stats data in a suitable format.
        AbilitiesText.text = "Abilities: " /*+ <Abilities Data> */;
        BaseStatsText.text = "Base Stats: " /*+ <Base Stats Data> */;
    }

    private void SaveData(string rawData)
    {
        string path = Application.dataPath + "/../data.json"; // The '/../' moves one directory up to the project root.

        // Write the json data to a file.
        try
        {
            File.WriteAllText(path, rawData);
        }
        catch(Exception ex)
        {
            Debug.LogError("Error saving Pokemon data: " + ex.Message);
        }
    }
}