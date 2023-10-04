using System.Linq;
using TMPro;
using UnityEngine;

public class PokemonUiManager : MonoBehaviour
{
    [Header("Managers")]
    public PokemonApiManager ApiManager;
    public PokemonDataManager DataManager;

    [Header("Inputs")]
    public TMP_InputField NameInput;

    [Header("Output")]
    public TMP_Text NameText;
    public TMP_Text PokedexNumberText;
    public TMP_Text TypeText;
    public TMP_Text HeightText;
    public TMP_Text WeightText;
    public TMP_Text BaseStatsText;
    public TMP_Text AbilitiesText;

    public void OnEnable()
    {
        DataManager.onDataChange += RefreshUiData;
    }

    public void OnDisable()
    {
        DataManager.onDataChange -= RefreshUiData;
    }

    public void SubmitInput()
    {
        // Get input
        string pokemonName = NameInput.text.ToLower();

        // Validate input
        if (!string.IsNullOrEmpty(pokemonName) && !pokemonName.All(char.IsLetterOrDigit))
        {
            Debug.LogError("Invalid Pokemon name");
            return;
        }

        // Send request for data
        ApiManager.RequestData(pokemonName);
    }

    private void RefreshUiData()
    {
        // Get data
        PokemonData data = DataManager.Data;

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
}
