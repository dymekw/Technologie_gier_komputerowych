using UnityEngine;
using UnityEngine.SceneManagement;

class ExampleGUIAspectsController : MonoBehaviour
{
    public static HealthSystem health_bar;

    public Rect HealthBarDimens;
    public bool VerticleHealthBar;
    public Texture HealthBubbleTexture;
    public Texture HealthTexture;
    public float HealthBubbleTextureRotation;

    public void Start()
    {
        health_bar = new HealthSystem(HealthBarDimens, VerticleHealthBar, HealthBubbleTexture, HealthTexture, HealthBubbleTextureRotation);
        health_bar.IncrimentBar(200);
        health_bar.Initialize();
    }

    public void OnGUI()
    {
        health_bar.DrawBar();
    }

    public void Update()
    {
        health_bar.Update();
        if (health_bar.getCurrentValue() <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}