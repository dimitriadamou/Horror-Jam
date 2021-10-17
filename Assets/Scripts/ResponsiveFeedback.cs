using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveFeedback : MonoBehaviour
{
    [SerializeField] SharedInt playerHealth;
    [SerializeField] NoArgEvent OnDamage;
    [SerializeField] GameObject DamageMask;
    [SerializeField] GameObject ShadowMask;

    private SpriteRenderer m_DamageMaskSprite;
    private SpriteRenderer m_ShadowMaskSprite;
    // Start is called before the first frame update

    private void OnEnable() {
        OnDamage.Callback += TakeDamage;
        playerHealth.Callback += OnPlayerHealthUpdate;
        m_DamageMaskSprite = DamageMask.GetComponent<SpriteRenderer>();
        m_ShadowMaskSprite = ShadowMask.GetComponent<SpriteRenderer>();
    }
    private void OnDisable() {
        OnDamage.Callback -= TakeDamage;    
        playerHealth.Callback -= OnPlayerHealthUpdate;
    }

    public void OnPlayerHealthUpdate(int value)
    {
        var alpha = 1f - ((float)value/100f);

        m_ShadowMaskSprite.color = new Color(
            m_ShadowMaskSprite.color.r,
            m_ShadowMaskSprite.color.g,
            m_ShadowMaskSprite.color.b,
            alpha
        );

    }

    public void TakeDamage()
    {
        StartCoroutine(FlashDamageMask());
    }

    public IEnumerator FlashDamageMask()
    {
        var oldColour = m_DamageMaskSprite.color;
        var noAlpha = new Color(
            oldColour.r,
            oldColour.g,
            oldColour.b,
            0f
        );
        m_DamageMaskSprite.color = noAlpha;

        while(!Mathf.Approximately(m_DamageMaskSprite.color.a, 1))
        {
            if(m_DamageMaskSprite.color.a > 0.5f) {
                break;
            }
            m_DamageMaskSprite.color = new Color(
                oldColour.r,
                oldColour.g,
                oldColour.b,
                m_DamageMaskSprite.color.a + (5f * Time.deltaTime)
            );
            yield return new WaitForEndOfFrame();
        }

        m_DamageMaskSprite.color = noAlpha;

        yield return null;
    }
}
