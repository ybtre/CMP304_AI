using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class BeaconAgent : Agent {
    private Rigidbody myRb;

    [SerializeField] private BeaconButton _button;
    [SerializeField] private Goal _goal;

    private void Awake() {
        myRb = GetComponent<Rigidbody>();
    }


    public override void OnEpisodeBegin() {
        transform.localPosition = new Vector3(Random.Range(-3f, 0f), 0.1f, Random.Range(-1.9f, +1.9f));
        _button.gameObject.transform.localPosition = new Vector3(_button.gameObject.transform.localPosition.x, _button.gameObject.transform.localPosition.y, Random.Range(-2f, +2f));
        
        if (_goal.isSpawned) {
            _goal.Despawn();
        }

        _button.ResetButton();
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(_button.CanUseButton() ? 1 : 0);

        Vector3 dirToBeaconButton = (_button.transform.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(dirToBeaconButton.x);
        sensor.AddObservation(dirToBeaconButton.z);
        
        sensor.AddObservation(_goal.isSpawned ? 1 : 0);

        if (_goal.isSpawned) {
            if (_goal != null) {
                Vector3 dirToBeacon = (_goal.transform.localPosition - transform.localPosition).normalized;
                sensor.AddObservation(dirToBeacon.x);
                sensor.AddObservation(dirToBeacon.z);
            }
        }
        else {
            // bracon is not spawned
            sensor.AddObservation(0f); // x
            sensor.AddObservation(0f); // y
        }
    }

    public override void OnActionReceived(ActionBuffers actions) {
        int moveX = actions.DiscreteActions[0]; // 0 = dont move; 1 = left; 2 = right
        int moveZ = actions.DiscreteActions[1]; // 0 = dont move; 1 = back; 2 = forward

        Vector3 addForce = new Vector3(0, 0, 0);

        switch (moveX) {
            case 0: addForce.x = 0f; break;
            case 1: addForce.x = -1f; break;
            case 2: addForce.x = +1f; break;
        } 
        
        switch (moveZ) {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -1f; break;
            case 2: addForce.z = +1f; break;
        }

        float moveSpeed = 2f;
        myRb.velocity = addForce * moveSpeed + new Vector3(0, myRb.velocity.y, 0) * Time.deltaTime;
        
        bool isUseButtonDown = actions.DiscreteActions[2] == 1;
        if (isUseButtonDown) {
            // use action
            Collider[] colliderArray = Physics.OverlapBox(transform.position, Vector3.one * .5f);
            foreach (Collider collider in colliderArray) {
                if (collider.TryGetComponent<BeaconButton>(out BeaconButton beaconButton)) {
                    if (beaconButton.CanUseButton()) {
                        beaconButton.UseButton();
                        AddReward(1f);
                        
                    }
                }
            }
        }
        
        AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"))) {
            case -1: discreteActions[0] = 1; break;
            case  0: discreteActions[0] = 0; break;
            case +1: discreteActions[0] = 2; break;
        }
        
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical"))) {
            case -1: discreteActions[1] = 1; break;
            case  0: discreteActions[1] = 0; break;
            case +1: discreteActions[1] = 2; break;
        }

        discreteActions[2] = Input.GetKey(KeyCode.E) ? 1 : 0;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<Goal>(out Goal goal)) {
            AddReward(1f);
            
            goal.Despawn();
            // Destroy(goal.gameObject);
            
            _button.ResetButton();
            EndEpisode();
        }

        if (other.gameObject.TryGetComponent<Wall>(out Wall wall)) {
            AddReward(-1f);

            EndEpisode();
        }
    }
}
