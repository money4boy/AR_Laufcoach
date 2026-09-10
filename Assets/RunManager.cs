using UnityEngine;
using TMPro;

public class RunManager : MonoBehaviour
{
    [Header("UI und Objekte (Startmenü)")]
    public GameObject setupPanel;
    public GameObject laufPartner;
    public TMP_InputField minPulsInput;
    public TMP_InputField maxPulsInput;

    [Header("HUD (Während des Laufs)")]
    public GameObject hudPanel;
    public TextMeshProUGUI pulsText;
    public TextMeshProUGUI infoText;

    [Header("Zusammenfassung (Ende)")]
    public GameObject summaryPanel;
    public TextMeshProUGUI dauerText;
    public TextMeshProUGUI durchschnittPulsText;

    [Header("Puls Simulation")]
    [Range(60, 200)] 
    public int simulierterPuls = 120; 

    private int zielMinPuls;
    private int zielMaxPuls;
    private bool laufAktiv = false;

    private float laufZeit = 0f;
    private float messTimer = 0f;
    private int pulsSumme = 0;
    private int pulsMessungen = 0;

    public void StartLauf()
    {
        int.TryParse(minPulsInput.text, out zielMinPuls);
        int.TryParse(maxPulsInput.text, out zielMaxPuls);

       
        laufZeit = 0f;
        pulsSumme = 0;
        pulsMessungen = 0;
        
        setupPanel.SetActive(false);
        summaryPanel.SetActive(false);
        hudPanel.SetActive(true);
        laufPartner.SetActive(true);
        
        laufAktiv = true;
    }

    
    public void BeendeLauf()
    {
        laufAktiv = false;
        
        
        hudPanel.SetActive(false);
        laufPartner.SetActive(false);
        summaryPanel.SetActive(true);

        
        int minuten = Mathf.FloorToInt(laufZeit / 60F);
        int sekunden = Mathf.FloorToInt(laufZeit - minuten * 60);
        dauerText.text = string.Format("Dauer: {0:00}:{1:00}", minuten, sekunden);

        int durchschnitt = 0;
        if (pulsMessungen > 0)
        {
            durchschnitt = pulsSumme / pulsMessungen;
        }
        durchschnittPulsText.text = "Ø Puls: " + durchschnitt;
    }

    void Update()
    {
        if (laufAktiv == false) return; 

        
        laufZeit += Time.deltaTime;
        messTimer += Time.deltaTime;

        
        if (messTimer >= 1f)
        {
            pulsSumme += simulierterPuls;
            pulsMessungen++;
            messTimer = 0f; 
        }

        Vector3 zielPosition;
        pulsText.text = "Puls: " + simulierterPuls;

        if (simulierterPuls < zielMinPuls)
        {
            zielPosition = new Vector3(6,-1, 15);
            pulsText.color = Color.blue;
        }
        else if (simulierterPuls > zielMaxPuls)
        {
            zielPosition = new Vector3(6, -1, -3f);
            pulsText.color = Color.red;
        }
        else
        {
            zielPosition = new Vector3(6, -1, 3f); 
            pulsText.color = Color.green;
        }

        laufPartner.transform.position = Vector3.Lerp(laufPartner.transform.position, zielPosition, Time.deltaTime * 2f);
    }
}