using System.Collections.Generic;

// Note: Depending on the how the project expands, it might be best to split these into new files
// Keeping it in a single file now for readability

[System.Serializable]
public class PokemonData
{
    public int id;
    public string name;
    public int height;
    public int weight;
    public List<PokemonTypeInfo> types;
}

[System.Serializable]
public class PokemonTypeInfo
{
    public PokemonTypeDetail type;
}

[System.Serializable]
public class PokemonTypeDetail
{
    public string name;
}
