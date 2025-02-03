using UnityEngine;

public class SurgeonMeleeAttack : MonoBehaviour
{
    private float cooldown = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && cooldown <= 0) {
            cooldown = 0.3f;
        }
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 0.5f);
        } else {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }
}
