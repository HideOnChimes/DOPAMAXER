using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PostSpriteBank", menuName = "FeedSystem/PostSpriteBank")]
public class PostSpriteBank : ScriptableObject
{
    public PostSprite[] postSprites;
    private Dictionary<string, Sprite> _cache;

    private void OnEnable()
    {
        _cache = new Dictionary<string, Sprite>();
        foreach (PostSprite p in postSprites)
        {
            _cache[p.name] = p.sprite;
        }
    }

    public Sprite GetSprite(string name)
    {
        if (_cache.TryGetValue(name, out Sprite sprite))
        {
            return sprite;
        }
        else return null;
    }

}

[System.Serializable]
public class PostSprite 
{
    public string name;
    public Sprite sprite;
}
