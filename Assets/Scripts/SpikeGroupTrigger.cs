using UnityEngine;

public class SpikeGroupTrigger : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 0.05f;

    private SpikesTrigger[] spikes;
    private bool triggered = false;

    void Awake()
    {
        // Parent altýndaki TÜM SpikesTrigger’larý alýr
        spikes = GetComponentsInChildren<SpikesTrigger>();

        Debug.Log("Spike count: " + spikes.Length);
    }

    void Update()
    {
        if (triggered || spikes.Length == 0) return;

        float distance = Mathf.Abs(player.position.x - spikes[0].transform.position.x);

        if (distance <= triggerDistance)
        {
            TriggerAll();
        }
    }

    void TriggerAll()
    {
        triggered = true;

        foreach (SpikesTrigger spike in spikes)
        {
            spike.Trigger();
        }
    }
}
