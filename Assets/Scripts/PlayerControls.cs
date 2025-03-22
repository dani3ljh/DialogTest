using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private DialogBox dialogBox;
    private Rigidbody2D rb;

    [Header("Data")]
    [SerializeField] private float speed;

    private NPC currentNPC;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        rb.velocity = speed * new Vector2(horiz, vert);

        if (Input.GetButtonDown("Interact") && currentNPC != null) {
            dialogBox.gameObject.SetActive(true);
            dialogBox.dialogs = currentNPC.dialogs;
            dialogBox.OpenDialogBox();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("NPC")) {
            currentNPC = collider.GetComponent<NPC>();
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.CompareTag("NPC")) {
            currentNPC = null;
        }
    }
}
