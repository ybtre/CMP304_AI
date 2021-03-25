using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.MLAgents.Policies;
using UnityEngine;

public class ChangeBrain : MonoBehaviour {
    [SerializeField] private BeaconAgent Agent;
    private BehaviorParameters agentBehav;

    [SerializeField] private NNModel BrainModel;

    private void Awake() {
        agentBehav = Agent.GetComponent<BehaviorParameters>();
    }

    public void SetBrainModel() {
        agentBehav.Model = BrainModel;
    }
}
