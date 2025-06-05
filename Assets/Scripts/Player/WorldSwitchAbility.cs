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

        // Obtener referencias desde el jugador
        worldA = player.worldA;
        worldB = player.worldB;

        // Asegurarse que el mundo A esté activo y B inactivo al iniciar
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
