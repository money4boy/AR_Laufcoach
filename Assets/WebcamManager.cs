using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour
{
    public RawImage backgroundImage;
    
    [Header("Kamera Auswahl")]
    [Tooltip("0 = Erste Kamera, 1 = Zweite Kamera (oft extern), etc.")]
    public int cameraIndex = 0; 

    void Start()
    {
        // Holt eine Liste aller angeschlossenen Kameras
        WebCamDevice[] devices = WebCamTexture.devices;

        // Gibt alle gefundenen Kameras zur Kontrolle in der Konsole aus
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("Kamera " + i + ": " + devices[i].name);
        }

        // Prüfen, ob wir überhaupt Kameras haben und der Index gültig ist
        if (devices.Length > 0 && cameraIndex < devices.Length)
        {
            // Startet ganz gezielt die Kamera mit unserem gewählten Index
            WebCamTexture webcam = new WebCamTexture(devices[cameraIndex].name);
            
            backgroundImage.texture = webcam;
            backgroundImage.material.mainTexture = webcam;
            
            webcam.Play();
        }
        else
        {
            Debug.LogError("Fehler: Keine Kamera mit der Nummer " + cameraIndex + " gefunden!");
        }
    }
}