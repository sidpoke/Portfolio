using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour {

    private Rigidbody2D rigid;
    public float offsetX;
    private float smoothX;
    public float offsetY;
    [Header("Parallax Setup")]
    public List<ParallaxLayer> parallaxObjects = new List<ParallaxLayer>();

    float lastxvel;

	// Use this for initialization
	void Start () {
        //Rigidbody und erste Position des Spielers festlegen
        rigid = PlayerController.me.moveCont.GetComponent<Rigidbody2D>();
        lastxvel = PlayerController.me.transform.position.x;

        //Texture offset einmal festlegen
        foreach (ParallaxLayer l in parallaxObjects)
        {
            l.mat.mainTextureOffset = new Vector2(offsetX * l.offsetXMultiplier, offsetY * l.offsetYMultiplier);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		//Verschiebung erfolgt, wenn der Spieler sich bewegt (Rigidbody.velocity erweist sich als unpraktisch)
		//In früheren Versuchen hat es mal funktioniert und mal nicht (und damit einfach weitergescrollt)
		//Die Methode des "delta position x" erweist sich als sehr komfortabel
		//Beim Y-Scroll wird etwas geschummelt, der Wert geht nie weiter als ein Hundertstel der y-Geschwindigkeit
		
        offsetX += Time.deltaTime * (PlayerController.me.transform.position.x - lastxvel) * (PlayerController.me.moveCont.wallFriction ? 0 : 1);
        smoothX = Mathf.SmoothStep(smoothX, offsetX, 0.7f); //Hintergründe bewegen sich per SmoothStep zum Offset. Warum SmoothStep? Sieht halt gut aus.
        offsetY = Mathf.SmoothStep(offsetY, rigid.velocity.y / 100, 0.3f);
        lastxvel = PlayerController.me.transform.position.x;

		//Hier werden die Parallax-Texturen mit Offset * Geschwindigkeit festgelegt (x und y)
        if (rigid.velocity.x != 0) {
            foreach (ParallaxLayer l in parallaxObjects)
            {
                l.mat.mainTextureOffset = new Vector2(smoothX * l.offsetXMultiplier, l.mat.mainTextureOffset.y);
            }
        }

        if(rigid.velocity.y != 0)
        {
            foreach (ParallaxLayer l in parallaxObjects)
            {
                l.mat.mainTextureOffset = new Vector2(l.mat.mainTextureOffset.x, offsetY * l.offsetYMultiplier);
            }
        }
	}

	//Einmal für jeden Layer die zugehörige Textur auf das Scroll-Objekt zeichnen lassen
    public void SetLayerTexture(string lay, Texture tex)
    {
        foreach(ParallaxLayer l in parallaxObjects)
        {
            if (l.LayerName == lay)
                l.tex = tex;
        }
    }
}

[System.Serializable]
public class ParallaxLayer
{
    public string LayerName; //Layer name halt. Zum unterscheiden.
    public Material mat; //Material und Textur werden im Editor gesetzt (Material hat Scroll-fähigkeiten!)
    public Texture tex;
    public float offsetXMultiplier; //Dieser Wert setzt die Scrollgeschwindigkeit des Layers fest 
    public float offsetYMultiplier; //Dieser Wert wird für Höhenunterschiede verwendet
}

/// ~ (script by Jonas Walter) ~ ///
/// Ein Parallax Scroller kann mehrere Hintergrund-layer verschieben, um den Effekt von Tiefe zu erzeugen
/// Man Kann hier