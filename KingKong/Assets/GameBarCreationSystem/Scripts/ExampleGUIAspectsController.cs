using UnityEngine;
using UnityEngine.SceneManagement;

class ExampleGUIAspectsController : MonoBehaviour
{
    public static HealthSystem health_bar;
    public static GlobeBarSystem mouse_hold_bar;

    public Rect HealthBarDimens;
    public bool VerticleHealthBar;
    public Texture HealthBubbleTexture;
    public Texture HealthTexture;
    public float HealthBubbleTextureRotation;

    public Rect MouseBarDimens;
    public bool VerticleMouseBar;
    public Texture MouseBubbleTexture;
    public Texture MouseTexture;
    public Texture MouseTexture2;
    public float MouseBubbleTextureRotation;

    private static bool blocked = false;

    public void Start()
    {
        health_bar = new HealthSystem(HealthBarDimens, VerticleHealthBar, HealthBubbleTexture, HealthTexture, HealthBubbleTextureRotation);
        health_bar.IncrimentBar(200);
        health_bar.Initialize();

        intanciateNewMouseBar();
    }

    public void OnGUI()
    {
        health_bar.DrawBar();
        mouse_hold_bar.DrawBar();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && !blocked)
        {
            mouse_hold_bar.IncrimentBar(-1);
        } else if (!Input.GetMouseButton(0))
        {
            mouse_hold_bar.IncrimentBar(1);
        }

        if(mouse_hold_bar.getCurrentValue() <= 0)
        {
            blocked = true;
            intanciateNewMouseBar();
        }

        if (blocked && mouse_hold_bar.getCurrentValue() == 200)
        {
            blocked = false;
            intanciateNewMouseBar();
        }

        health_bar.Update();
        mouse_hold_bar.Update();

        if (health_bar.getCurrentValue() <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static bool isBlocked()
    {
        return blocked;
    }

    private void intanciateNewMouseBar()
    {
        if (blocked)
        {
            mouse_hold_bar = new GlobeBarSystem(MouseBarDimens, VerticleMouseBar, MouseBubbleTexture, MouseTexture2, MouseBubbleTextureRotation);
            mouse_hold_bar.Initialize();
            mouse_hold_bar.IncrimentBar(-100);
        } else
        {
            mouse_hold_bar = new GlobeBarSystem(MouseBarDimens, VerticleMouseBar, MouseBubbleTexture, MouseTexture, MouseBubbleTextureRotation);
            mouse_hold_bar.Initialize();
            mouse_hold_bar.IncrimentBar(100);
        }
    }
}