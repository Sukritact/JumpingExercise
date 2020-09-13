using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_jump : MonoBehaviour
{
    private enum State { Start, S1, S2, S3, S4, S5 }
    [SerializeField] private State _state;
    [SerializeField] private GameObject _stair;
    [SerializeField] private GameObject _floor;    
    
    public float jumpUpVelocity;   // Velocity on y-axis


    // Start is called before the first frame update
    void Start()
    {
        //  1
        _state = State.Start;         
    }

    // Update is called once per frame
    void Update()
    {
        // stateAction();

        //  5
        if(Mathf.Approximately(GetComponent<Rigidbody>().velocity.y,  0.0f)){
             stateAction();
         }


        // Final
        //if(Mathf.Approximately(GetComponent<Rigidbody>().velocity.y,  0.0f) && Input.GetButtonDown("Jump") == true){
        //    stateAction();
        //}
    }

    // 2
    void stateAction() {
        switch (_state)
        {
            case State.Start:
                jumpTo(_stair.transform.Find("S1").position);
                _state = State.S1;
                break;
            case State.S1:
                jumpTo(_stair.transform.Find("S2").position);
                _state = State.S2;
                break;
            case State.S2:
                jumpTo(_stair.transform.Find("S3").position);
                _state = State.S3;
                break;
            case State.S3:
                jumpTo(_stair.transform.Find("S4").position);
                _state = State.S4;
                break;
            case State.S4:
                jumpTo(_stair.transform.Find("S5").position);
                _state = State.S5;
                break;
            case State.S5:
                jumpTo(_floor.transform.position);
                _state = State.Start;
                break;    
        }
    }

    //  3
    void jumpTo(Vector3 point){
        Vector3 direction = point - transform.position;
        float time = timeToLand(point);
        float vX = direction.x / time;
        float vZ = direction.z / time;

        GetComponent<Rigidbody>().velocity = new Vector3(vX, jumpUpVelocity, vZ);
    }

    //  4
    float timeToLand(Vector3 point){
        float g = Physics.gravity.y;
        float sqrt = Mathf.Sqrt(Mathf.Abs(jumpUpVelocity * jumpUpVelocity - 2 * g * (transform.position - point).y ));
        return Mathf.Min(-jumpUpVelocity + sqrt, -jumpUpVelocity - sqrt ) / g;
    }
}
