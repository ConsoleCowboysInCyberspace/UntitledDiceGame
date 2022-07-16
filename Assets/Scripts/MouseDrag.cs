using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{   
    [SerializeField] private HingeJoint2D _joint;
    [SerializeField] private GameObject _movementAnchor;

    [SerializeField] private float spintorque = 0.1f;
    
    private GameObject _selectedDie;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            dropDie();
            if (trySelectDie())
            {
                pinDie();
            }
        }
    }
    
    void FixedUpdate()
    {
        // parent movement anchor
        _movementAnchor.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                                     Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        
    }

    // uses raycast to select dice
    bool trySelectDie()
    {  
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                                     Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        if (hit)
        {
            GameObject go = hit.transform.gameObject;
            if (go.tag == "Dice")
            {
                _selectedDie = go;
                print("Die Selected");
                return true;
            }
        }

        print("die not found");
        return false;
    }

    // drop dice + physics kick
    void dropDie()
    {
        _joint.connectedBody = null;
        
        // possibly not being triggered because same physics tick as unpin
        // also add torque is not impulsive for some reason

        // Rigidbody2D diebody = _selectedDie.GetComponent<Rigidbody2D>();
        // diebody.AddTorque(spintorque, ForceMode2D.Impulse);
        // diebody.AddForce(Vector2.up * 10.5f, ForceMode2D.Impulse);

        if (_selectedDie)
        {
            // reenable die collider
            Collider2D collider = _selectedDie.gameObject.GetComponent<Collider2D>();
            collider.enabled = true;
        }

        _selectedDie = null;
    }

    // physics pins the die when selected
    void pinDie()
    {
        // TEST 
        // dropDie();
        _joint.connectedBody = _selectedDie.GetComponent<Rigidbody2D>();

        // remove die physics
        Collider2D collider = _selectedDie.gameObject.GetComponent<Collider2D>();
        collider.enabled = false;

    }

}
