using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour {

    PlayerController playCont;
    public Image maxHealthBar;
    public Gradient colorGradient;
    public float valueOfQuotient;
    int health;
    int maxHealth;
    int globalMaxHealth;
    Image img;

	void Start () {
        //PlayerController und Image Komponente einlesen
        playCont = PlayerController.me;
        img = GetComponent<Image>();
	}
	
	void Update () {
		//Kein Spieler? Kein Script.
        if (playCont == null)
            return;

		//Hier werden Werte aus dem PlayerController abgerufen.
		//Es gibt einen Globalen Maximalwert (der Maximalwert, den man aus Upgrades kriegen kann)
		//Einen derzeitigen Wert, der angibt, wieviele Leben der Spieler noch hat
		//Und einen Maximalwert, den man durch Upgrades erweitern kann.
		
        health = (int)Mathf.MoveTowards(health, playCont.healthPoints, 3f);
        maxHealth = playCont.currentMaxHealth;
        globalMaxHealth = playCont.globalMaxHealth;

		//Wir rechnen einen Prozentsatz aus (leben/maxLeben), dann kann man direkt einen Gradient ausrechnen und einsetzen
        valueOfQuotient = SafeDivision(health, maxHealth);
        img.color = colorGradient.Evaluate(valueOfQuotient);

		//Neuen Prozentsatz aus Globalne Maximalwert und Leben in das Fillamount eingeben, Fertig.
		//Jetzt wird die Lebensanzeige nicht ganz Kreisförmig dargestellt, damit ein Upgrade bemerkbar ist.
		//Die Komplette anzeige ist halb Voll wenn man zb. 100 von 200 maximal-Leben hat.
        img.fillAmount = SafeDivision(health, globalMaxHealth);
        maxHealthBar.fillAmount = SafeDivision(maxHealth, globalMaxHealth);
    }

    //Teilen durch Null verhindern!
    public float SafeDivision(float Numerator, float Denominator)
    {
		//Nenner = 0? Abbruch, Null herausgeben damit der Gradient auch bei 0 bleibt
        return (Denominator == 0) ? 0 : Numerator / Denominator;
		
		//Mathematiker hassen ihn, er setzt einfach eine 0 hin. DER WELTUNTERGANG. (Humor enthalten)
    }
}

/// ~ (script by Jonas Walter) ~ ///
/// Dieses Script steuert einen kreisförmigen Image-Cutout an (Unity UI Bibliothek "using UnityEngine.UI;")
/// In einem "Gradient" kann man einen float Wert zwischen 0 und 1 zu einer Farbe im Farbverlauf verwerten lassen
/// Dadurch erhält man z.B. wie hier eine schöne Kreisförmige Lebensanzeige, die ihre Farbe verändern kann
