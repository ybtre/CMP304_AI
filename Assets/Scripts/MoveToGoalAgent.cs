using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class MoveToGoalAgent : Agent {
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer[] floorMeshRenderer;

  
    public override void OnEpisodeBegin() {
        transform.localPosition = new Vector3(Random.Range(-3f, 0f),0.1f, Random.Range(-1.9f, +1.9f));
        targetTransform.localPosition = new Vector3(Random.Range(0.6f, 3.9f),0.1f, Random.Range(-2f, +2f));
    }
    
    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveY) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Goal>(out Goal goal)) {
            SetReward(+1f);
            
            foreach (var renderer in floorMeshRenderer) {
                renderer.material = winMaterial;
            }
            
            EndEpisode();
        }
        
        if (other.TryGetComponent<Wall>(out Wall wall)) {
            SetReward(-1f);
            
            foreach (var renderer in floorMeshRenderer) {
                renderer.material = loseMaterial;
            }
            
            EndEpisode();
        }
    }
}
