using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image bar;

    public Gradient gradient;

    public PlayerData playerData;

    public void Fill(float amoutNormalized) {
        bar.fillAmount = amoutNormalized;
        // change health's bar's color according to the gradient
        bar.color = gradient.Evaluate(amoutNormalized);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fill((float)playerData.currentLifePoints / (float)playerData.maxLifePoints);
    }
}
