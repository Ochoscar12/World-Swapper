using UnityEngine;

public class WorldSwitchAbility : MonoBehaviour
{
    private GameObject worldA;
    private GameObject worldB;
    private bool worldBActive = false;

    private PlayerController player;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        worldA = player.worldA;
        worldB = player.worldB;

        if (worldA != null) worldA.SetActive(true);
        if (worldB != null) worldB.SetActive(false);
    }

    void OnEnable()
    {
        player.inputActions.Player.SwitchWorld.performed += _ => ToggleWorldB();
    }

    void OnDisable()
    {
        player.inputActions.Player.SwitchWorld.performed -= _ => ToggleWorldB();
    }

    void ToggleWorldB()
    {
        if (worldB == null) return;

        worldBActive = !worldBActive;
        worldB.SetActive(worldBActive);
    }
}
