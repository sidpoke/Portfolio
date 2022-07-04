using UnityEngine;
using System.Collections;

public class CameraOrientation : MonoBehaviour {
    public float lookDistance;
    public float lookSpeed;

    public bool playerFixed;
    public float xpos = 0f;
    public float ypos = 0f;
    public float zpos = 0f;

    private Vector3 position;
	
	void FixedUpdate () {
        //Lokaler 3D Vektor auf Werte xpos, ypos und zpos setzen und die Kamera zu den gegebenen Koordinaten durch Mathf.Lerp verschieben
		//Anzumerken wäre, dass die Kamera in einem Parent-Objekt als Child den Nullvektor als Position hat
		//Hier wird auch die Geschwindigkeit "lookSpeed" der Verschiebung im Lerp benutzt
		
        position = new Vector3(xpos, ypos, zpos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, position, lookSpeed * Time.smoothDeltaTime);
	}

    void Update()
    {
        if(playerFixed && PlayerController.me != null)    //Verschiebung auf Bewegungsrichtung anpassen (Rechts laufen = mehr Sicht nach rechts, etc.)
        {
			//Lookdistance ist die Entfernung, die man vorausschauen darf
            xpos = Mathf.Lerp(xpos, lookDistance * Input.GetAxisRaw("Horizontal") * ((PlayerController.me.moveCont.disableMovement) ? 0 : 1), 0.2f);
            ypos = Mathf.Lerp(ypos, lookDistance * Input.GetAxisRaw("Vertical") * ((PlayerController.me.moveCont.disableMovement) ? 0 : 1), 0.2f);
        }
    }

    public void SetRecoil(Vector3 recoil)
    {
		//Recoil ist hier sehr einfach, man setzt einfach einen anderen Wert, die Kamera erzeugt einen ruckartigen Bewegungseffekt, 
		//da sie direkt zum Vektor3 "position" zurückschlägt. Das Zurückschlagen geht aber nicht so schnell, da die Kamera Lerp verwendet.
        transform.position += recoil;
    }
}

/// ~ (script by Jonas Walter) ~ ///
/// Dieses Script setzt eine kleine x-Achsen Verschiebung auf die Kamera,
/// damit der Spieler auf Gefahren reagieren und vorausschauen kann
/// 
/// Notiz: Zu schnelle Bewegungen verursachen Kopfschmerzen!
/// Notiz2: Werte werden im Editor gesetzt