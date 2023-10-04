using System;
using System.IO;
using UnityEngine;

public class PokemonDataManager : MonoBehaviour
{
    public PokemonData Data { private set; get; }

    public delegate void OnDataChange();
    public OnDataChange onDataChange;

    public void SetData(string rawData)
    {
        // Convert data
        Data = JsonUtility.FromJson<PokemonData>(rawData);

        // Save data
        SaveData(rawData);

        // Invoke onDataChange so elements dependent on the data can update
        onDataChange?.Invoke();
    }

    private void SaveData(string rawData)
    {
        // Create data path
        string path = Application.dataPath + "/../data.json"; // The '/../' moves one directory up to the project root.

        try
        {
            // Write the json data to a file.
            File.WriteAllText(path, rawData);
        }
        catch(Exception ex)
        {
            // Log file write errors
            Debug.LogError(ex);
        }
    }
}
