using UnityEngine;

public class GameOverUi : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        SpawnManager.Instance.OnBaseDestroyed += HandleBaseDestroyed;
    }

    private void OnDestroy()
    {
        SpawnManager.Instance.OnBaseDestroyed -= HandleBaseDestroyed;
    }

    private void HandleBaseDestroyed()
    {
        gameOverPanel.SetActive(true);
    }
}
