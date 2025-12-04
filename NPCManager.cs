using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("List NPC Prefabs")]
    public GameObject[] npcList;    // isi prefab NPC Cowok & Cewek

    [Header("Spawn Point NPC")]
    public Transform spawnPoint;

    private GameObject currentNPC;
    private Animator currentAnimator;
    private int lastSpawn = -1;

    void Start()
    {
        SpawnRandomNPC();
    }

    public void SpawnRandomNPC()
    {
        // Hapus NPC lama
        if (currentNPC != null)
            Destroy(currentNPC);

        // Pilih NPC acak (tidak sama seperti sebelumnya)
        int next = Random.Range(0, npcList.Length);
        while (next == lastSpawn && npcList.Length > 1)
        {
            next = Random.Range(0, npcList.Length);
        }
        lastSpawn = next;

        // Spawn NPC baru
        currentNPC = Instantiate(npcList[next], spawnPoint.position, spawnPoint.rotation);

        // Ambil animator NPC
        currentAnimator = currentNPC.GetComponent<Animator>();

        Debug.Log("NPC Spawned: " + currentNPC.name);
    }

   
    //      FUNGSI ANIMASI NPC
    public void PlayIdle()
    {
        if (currentAnimator != null)
            currentAnimator.SetTrigger("Idle");
    }

}
