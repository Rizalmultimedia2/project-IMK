
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        Player.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
}
