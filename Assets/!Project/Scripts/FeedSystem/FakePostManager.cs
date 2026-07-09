using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;

public class FakePostManager : MonoSingleton<FakePostManager>
{
    public List<FakePost> fakePosts = new List<FakePost>();
    public Image[] screenPosts;
    private FakePost previousPost, actualPost, nextPost;

    async void StartScroll() 
    {

        actualPost = fakePosts[Random.Range(0, fakePosts.Count)];
        nextPost = await NextScrollScreen();
        previousPost = await NextScrollScreen();

    }

    async UniTask<FakePost> NextScrollScreen()
    {
        if(fakePosts.Count == 1)
        {
            return fakePosts[0];
        }
        FakePost aux = fakePosts[Random.Range(0, fakePosts.Count)];
        while(aux == actualPost)
        {
            aux = fakePosts[Random.Range(0, fakePosts.Count)];
            await UniTask.Delay(1);
        }
        return aux;

    }

}


