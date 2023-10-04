using System.Collections.Generic;

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
