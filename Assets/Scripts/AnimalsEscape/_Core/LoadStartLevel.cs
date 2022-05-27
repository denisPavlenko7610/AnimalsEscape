using AnimalsEscape.Core.SceneManagement;
using UnityEngine;
using Zenject;

public class LoadStartLevel : MonoBehaviour
{
    private LevelLoader LevelLoader;

    [Inject]
    public void Construct(LevelLoader LevelLoader)
    {
        this.LevelLoader = LevelLoader;
    }

    private void Start()
    {
        LevelLoader.LoadNextLevel();
    }
}
