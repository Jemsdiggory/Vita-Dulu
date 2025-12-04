using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CookingUIManager : MonoBehaviour
{
    [Header("Panel Step Masak (1-4)")]
    public List<GameObject> cookingPanels = new List<GameObject>();

    [Header("UI Tambahan")]
    public GameObject siapkanPesananButton;
    public GameObject buttonLanjutTerakhir;
    public GameObject textResponNpc;
    public Text textPesananPanel;
    public GameObject textPesananNpc;

    [Header("UI Image Bubble Text NPC")]
    public Image bubblePesanan; //image bubble text pesanan npc
    public Image bubbleMasak; // image bubble text pesanan panel
    public Image bubbleRespon; // image bubble text respon npc

    [Header("Tombol Tambahan")]
    public GameObject buttonNpcSelanjutnya;
    public GameObject buttonTutupToko;

    [Header("Typing Effect NPC")]
    public Text npcResponseText;
    public float typingSpeed = 0.05f;

    [Header("Objek Bahan (Bahan 1 - 4)")]
    public GameObject bahan1;
    public GameObject bahan2;
    public GameObject bahan3;
    public GameObject bahan4;

    [Header("Objek Menu Jadi")]
    public GameObject CenilObj;
    public GameObject KleponObj;
    public GameObject NagaSariObj;
    public GameObject PutuAyuObj;

    private int currentStep = 0;
    private string currentOrder;
    private bool isInCookingMode = false;

    private string[] selectedIngredients = new string[4];
    private Dictionary<string, string[]> recipes = new Dictionary<string, string[]>();
    private NpcOrderSistem npcRef;

    private int orderCount = 0;

    [Header("NPC System")]
    public NPCManager npcManager;     //referensi dari script npc manager

    [Header("Sound Effect Text NPC")]
    public AudioSource sfxSource;
    public AudioClip textPopupSFX;



    void Start()
    {
        recipes["Naga Sari"] = new string[] { "Beras", "Santan", "Gula Pasir", "Pisang" };
        recipes["Klepon"] = new string[] { "Ketan", "Air", "Gula Jawa", "Kelapa Parut" };
        recipes["Cenil"] = new string[] { "Tapioka", "Air", "Gula Pasir", "Kelapa Parut" };
        recipes["Putu Ayu"] = new string[] { "Beras", "Santan", "Gula Pasir", "Kelapa Parut" };

        foreach (GameObject panel in cookingPanels)
            if (panel != null) panel.SetActive(false);

        if (textResponNpc != null) textResponNpc.SetActive(false);
        if (textPesananPanel != null) textPesananPanel.gameObject.SetActive(false);
        if (buttonNpcSelanjutnya != null) buttonNpcSelanjutnya.SetActive(false);
        if (buttonTutupToko != null) buttonTutupToko.SetActive(false);

        HideAllBubbles();
        HideAllBahan();
        HideAllMenu();
    }

    // =======================
    // BUBBLE HELPER
    // =======================

    void ShowBubble(Image bubble)
    {
        if (bubble != null)
            bubble.gameObject.SetActive(true);
    }

    void HideBubble(Image bubble)
    {
        if (bubble != null)
            bubble.gameObject.SetActive(false);
    } //

    //void HideAllBubbles()
    //{
        //HideBubble(bubblePesanan);
        //HideBubble(bubbleMasak);
        //HideBubble(bubbleRespon);
    //}

    public void PlayTextSFX()
    {
        if (sfxSource != null && textPopupSFX != null)
            {
                sfxSource.PlayOneShot(textPopupSFX);
            }
    }


    private void HideAllBahan()
    {
        if (bahan1) bahan1.SetActive(false);
        if (bahan2) bahan2.SetActive(false);
        if (bahan3) bahan3.SetActive(false);
        if (bahan4) bahan4.SetActive(false);
    }

    private void HideAllMenu()
    {
        if (CenilObj) CenilObj.SetActive(false);
        if (KleponObj) KleponObj.SetActive(false);
        if (NagaSariObj) NagaSariObj.SetActive(false);
        if (PutuAyuObj) PutuAyuObj.SetActive(false);
    }
    // =======================
    // BUBBLE TEXT CONTROLLER
    // =======================

    public void ShowNpcOrderBubble()
        {
            if (bubblePesanan != null)
            bubblePesanan.gameObject.SetActive(true);

            if (bubbleMasak != null)
            bubbleMasak.gameObject.SetActive(false);

            if (bubbleRespon != null)
            bubbleRespon.gameObject.SetActive(false);
        }

    public void ShowMasakBubble()
        {
            if (bubblePesanan != null)
            bubblePesanan.gameObject.SetActive(false);

            if (bubbleMasak != null)
            bubbleMasak.gameObject.SetActive(true);

            if (bubbleRespon != null)
            bubbleRespon.gameObject.SetActive(false);
        }

    public void ShowResponBubble()
        {
            if (bubblePesanan != null)
            bubblePesanan.gameObject.SetActive(false);

            if (bubbleMasak != null)
            bubbleMasak.gameObject.SetActive(false);

            if (bubbleRespon != null)
            bubbleRespon.gameObject.SetActive(true);
        }

    public void HideAllBubbles()
        {
            if (bubblePesanan != null)
            bubblePesanan.gameObject.SetActive(false);

            if (bubbleMasak != null)
            bubbleMasak.gameObject.SetActive(false);

            if (bubbleRespon != null)
            bubbleRespon.gameObject.SetActive(false);
        }


    public void OpenCookingPanel(string order, NpcOrderSistem npc)
    {
        if (isInCookingMode) return;
        isInCookingMode = true;

        currentOrder = order;
        npcRef = npc;
        currentStep = 0;

        selectedIngredients = new string[4];

        HideAllBahan();
        HideAllMenu();

        if (textPesananNpc != null) textPesananNpc.SetActive(false);
        HideBubble(bubblePesanan);   // ✅ bubble pesanan NPC


        if (siapkanPesananButton != null) siapkanPesananButton.SetActive(false);

       if (textPesananPanel != null)
        {
            textPesananPanel.gameObject.SetActive(true);
            textPesananPanel.text = "Pesanan: " + currentOrder;

            PlayTextSFX(); // 🔊
            ShowBubble(bubbleMasak);   // ✅ bubble panel masak
        }


        if (cookingPanels.Count > 0)
            cookingPanels[0].SetActive(true);

        if (buttonLanjutTerakhir != null) buttonLanjutTerakhir.SetActive(false);
        if (textResponNpc != null) textResponNpc.SetActive(false);
        if (buttonNpcSelanjutnya != null) buttonNpcSelanjutnya.SetActive(false);
    }

    public void SelectIngredient(Button button)
    {
        if (!isInCookingMode) return;

        string selectedTag = button.tag;
        selectedIngredients[currentStep] = selectedTag;

        HideAllBahan();

        switch (currentStep)
        {
            case 0: if (bahan1) bahan1.SetActive(true); break;
            case 1: if (bahan2) bahan2.SetActive(true); break;
            case 2: if (bahan3) bahan3.SetActive(true); break;
            case 3: if (bahan4) bahan4.SetActive(true); break;
        }

        if (currentStep < cookingPanels.Count - 1)
        {
            cookingPanels[currentStep].SetActive(false);
            currentStep++;
            cookingPanels[currentStep].SetActive(true);

            if (currentStep == cookingPanels.Count - 1 && buttonLanjutTerakhir != null)
                buttonLanjutTerakhir.SetActive(true);
        }
        else
        {
            if (buttonLanjutTerakhir != null)
                buttonLanjutTerakhir.SetActive(true);
        }
    }

    public void NextStep()
    {
        if (!isInCookingMode) return;

        if (string.IsNullOrEmpty(selectedIngredients[currentStep]))
            return;

        if (buttonLanjutTerakhir != null)
            buttonLanjutTerakhir.SetActive(false);

        CheckRecipeResult();
    }

    private void CheckRecipeResult()
    {
        bool correct = true;
        string[] correctRecipe = recipes[currentOrder];

        for (int i = 0; i < correctRecipe.Length; i++)
        {
            if (selectedIngredients[i] != correctRecipe[i])
                correct = false;
        }

        SpawnMenuJadi();
        EndCookingSession(correct);
    }

    private void SpawnMenuJadi()
    {
        HideAllBahan();
        HideAllMenu();

        if (currentOrder == "Cenil") CenilObj.SetActive(true);
        if (currentOrder == "Klepon") KleponObj.SetActive(true);
        if (currentOrder == "Naga Sari") NagaSariObj.SetActive(true);
        if (currentOrder == "Putu Ayu") PutuAyuObj.SetActive(true);
    }

    private void EndCookingSession(bool correct)
    {
        foreach (GameObject panel in cookingPanels)
            if (panel != null) panel.SetActive(false);

        if (textPesananPanel != null)
            textPesananPanel.gameObject.SetActive(false);

        if (siapkanPesananButton != null)
            siapkanPesananButton.SetActive(false);

        if (textPesananNpc != null)
            textPesananNpc.SetActive(false);

        if (textResponNpc != null)
            textResponNpc.SetActive(true);

        
        HideBubble(bubblePesanan);
        HideBubble(bubbleMasak);
        ShowBubble(bubbleRespon);   // ✅ bubble respon NPC muncul


        PlayTextSFX();   // 🔊 sound respon NPC

        StartCoroutine(TypeNpcResponse(correct));

        isInCookingMode = false;
        orderCount++;
    }

    private IEnumerator TypeNpcResponse(bool correct)
    {
        string fullText = correct ?
            "Waaaw enak banget, makasih yaa! 😊" :
            "Kamu salah bikin, aku marah nih huh 😡";

        ShowBubble(bubbleRespon);
        npcResponseText.text = "";

        foreach (char c in fullText)
        {
            npcResponseText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1f);

        if (orderCount < 5)
        {
            if (buttonNpcSelanjutnya != null)
                buttonNpcSelanjutnya.SetActive(true);
        }
        else
        {
            if (buttonNpcSelanjutnya != null)
                buttonNpcSelanjutnya.SetActive(false);

            if (buttonTutupToko != null)
                buttonTutupToko.SetActive(true);
        }
    }

    public void NextNpcOrder()
    {
        HideAllBubbles();


        if (textResponNpc != null)
            textResponNpc.SetActive(false);

        if (buttonNpcSelanjutnya != null)
            buttonNpcSelanjutnya.SetActive(false);

        if (orderCount >= 5)
        {
            if (buttonTutupToko != null)
                buttonTutupToko.SetActive(true);

            return;
        }

        HideAllMenu();
        HideAllBahan();

        if (textPesananNpc != null)
            textPesananNpc.SetActive(true);

        if (siapkanPesananButton != null)
            siapkanPesananButton.SetActive(true);

        if (npcRef != null)
            npcRef.GenerateNewOrder();

        npcManager.SpawnRandomNPC(); //spawn npc random dari script npc manager
        npcManager.PlayIdle();  //ini juga dari npc manager, buat play idle
        ShowBubble(bubblePesanan);   // ✅ bubble pesanan NPC muncul lagi

    }

    public void TutupToko()
    {
        foreach (GameObject panel in cookingPanels)
            if (panel != null) panel.SetActive(false);

        HideAllBahan();
        HideAllMenu();

        if (textPesananPanel != null) textPesananPanel.gameObject.SetActive(false);
        if (textPesananNpc != null) textPesananNpc.SetActive(false);
        if (textResponNpc != null) textResponNpc.SetActive(false);
        if (siapkanPesananButton != null) siapkanPesananButton.SetActive(false);

        HideAllBubbles();


        SceneManager.LoadScene("Home Page");
    }
}
