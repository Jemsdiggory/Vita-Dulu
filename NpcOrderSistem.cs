using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NpcOrderSistem : MonoBehaviour
{
    [Header("UI Reference")]
    public Text orderText; // teks pesanan NPC
    public Button buttonSiapkanPesanan; // tombol "Siapkan Pesanan"

    [Header("Orders")]
    private string[] orders = { "Klepon", "Putu Ayu", "Cenil", "Naga Sari" };
    [HideInInspector] public string currentOrder;

    [Header("Typing Effect Settings")]
    public float typingSpeed = 0.05f;
    public float delayBeforeOrder = 2f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip popupSFX;

    [Header("Manager Reference")]
    public CookingUIManager cookingUIManager; // referensi ke UI Manager

    private Coroutine typingCoroutine;

    void Start()
    {
        if (buttonSiapkanPesanan != null)
        {
            buttonSiapkanPesanan.interactable = false;
            buttonSiapkanPesanan.onClick.RemoveAllListeners();
            buttonSiapkanPesanan.onClick.AddListener(OnClickSiapkan);
        }

        if (orderText != null)
            orderText.text = "";

        // Mulai pesanan pertama
        StartCoroutine(DelayBeforeShowOrder());
    }

    private IEnumerator DelayBeforeShowOrder()
    {
        yield return new WaitForSeconds(delayBeforeOrder);
        GenerateOrder();
    }

    // 🔁 Fungsi utama untuk menghasilkan pesanan baru
    public void GenerateOrder()
    {
        currentOrder = orders[Random.Range(0, orders.Length)];
        string fullText = "Aku mau pesan " + currentOrder;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (audioSource != null && popupSFX != null)
            audioSource.PlayOneShot(popupSFX);

        if (cookingUIManager != null)
        {
            cookingUIManager.ShowNpcOrderBubble(); // bubble muncul
            cookingUIManager.PlayTextSFX();        // 🔊 PLAY SOUND
        }


        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string textToType)
    {
        if (orderText == null) yield break;

        orderText.text = "";
        foreach (char c in textToType)
        {
            orderText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // aktifkan tombol setelah teks selesai diketik
        if (buttonSiapkanPesanan != null)
            buttonSiapkanPesanan.interactable = true;
    }

    public void OnClickSiapkan()
    {
        Debug.Log("Mulai menyiapkan pesanan: " + currentOrder);

        if (cookingUIManager != null)
        {
            cookingUIManager.OpenCookingPanel(currentOrder, this); // kirim referensi NPC
        }
        else
        {
            Debug.LogWarning("CookingUIManager belum dihubungkan ke NPC!");
        }

        if (buttonSiapkanPesanan != null)
            buttonSiapkanPesanan.interactable = false;
    }

    //Fungsi ini dipanggil dari CookingUIManager saat tombol “NPC Selanjutnya” ditekan
    public void GenerateNewOrder()
    {
    // Munculkan pesanan baru langsung (tanpa delay)
    currentOrder = orders[Random.Range(0, orders.Length)];
    string fullText = "Aku mau pesan " + currentOrder;

    // Reset teks dan efek suara
    if (typingCoroutine != null)
        StopCoroutine(typingCoroutine);

    if (audioSource != null && popupSFX != null)
        audioSource.PlayOneShot(popupSFX);

    typingCoroutine = StartCoroutine(TypeText(fullText));

    Debug.Log("Pesanan baru dari NPC: " + currentOrder);
    }

}
