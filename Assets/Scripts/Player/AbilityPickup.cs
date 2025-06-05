using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public enum AbilityType { DoubleJump, Dash, WallJump, WorldSwitch }
    public AbilityType abilityToUnlock;
    public GameObject wallCheck;
    void Awake()
    {
 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                switch (abilityToUnlock)
                {
                    case AbilityType.DoubleJump:
                        if (player.GetComponent<DoubleJumpAbility>() == null)
                            player.gameObject.AddComponent<DoubleJumpAbility>();
                        break;

                    case AbilityType.Dash:
                        if (player.GetComponent<DashAbility>() == null)
                            player.gameObject.AddComponent<DashAbility>();
                        break;

                    case AbilityType.WallJump:
                        if (player.GetComponent<WallJumpAbility>() == null)
                        player.gameObject.AddComponent<WallJumpAbility>();
                            wallCheck.SetActive(true);
                        break;

                    case AbilityType.WorldSwitch:
                        if (player.GetComponent<WorldSwitchAbility>() == null)
                            player.gameObject.AddComponent<WorldSwitchAbility>();
                        break;
                }

                Destroy(gameObject); // Destruye el objeto una vez recogido
            }
        }
    }
}

