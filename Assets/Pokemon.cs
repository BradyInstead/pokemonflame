using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text pokedexNumberText;
    public TMP_Text typeText;
    public TMP_Text heightText;
    public TMP_Text weightText;
    public TMP_Text descriptionText;
    public TMP_Text baseStatsText;
    public TMP_Text abilitiesText;
    public TMP_InputField searchInputField;

    public void Start()
    {
        try
        {
            GetStuff();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void GetStuff()
    {
        StartCoroutine(FetchPokemonData());
    }

    IEnumerator FetchPokemonData()
    {
        string pokemonName = searchInputField.text.ToLower(); // Assume the user inputs the Pokémon name here.
        string url = "https://pokeapi.co/api/v2/pokemon/" + pokemonName;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        string jsonResult = request.downloadHandler.text;
        Debug.Log(request.downloadHandler.text);

        PokemonData data = JsonUtility.FromJson<PokemonData>(jsonResult);
        SaveData(request.downloadHandler.text);

        // Populate the UI with data.
        nameText.text = data.name.ToUpper();
        pokedexNumberText.text = "Pokedex Number: " + data.id.ToString();
        typeText.text = "Type: " + data.types[0].type.name; // Only taking the first type for demonstration.
        heightText.text = "Height: " + data.height.ToString();
        weightText.text = "Weight: " + data.weight.ToString();

        // Assume you have fetched Abilities and Base Stats data in a suitable format.
        abilitiesText.text = "Abilities: " /*+ <Abilities Data> */;
        baseStatsText.text = "Base Stats: " /*+ <Base Stats Data> */;
       
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
    
    public void SaveData(string data)
    {
        string path = Application.dataPath + "/../data.json"; // The '/../' moves one directory up to the project root.

        // Write the json data to a file.
        File.WriteAllText(path, data);
    }
}
