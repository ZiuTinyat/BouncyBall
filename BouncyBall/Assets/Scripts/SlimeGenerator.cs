using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour {

    public int LayerNumber = 3;
    public int LayerUnitNumber = 6;
    public float UnitScale = 0.1f;
    public float SqueezeRatio = 2f;

    [SerializeField] GameObject SlimeUnit;

	// Use this for initialization
	void Start () {
        GenerateSlime();
	}

    private void GenerateSlime() {
        GameObject centerUnit = Instantiate(SlimeUnit, transform.position, transform.rotation, transform);
        GameObject[] lastLayerUnits = new GameObject[LayerUnitNumber];
        GameObject[] thisLayerUnits = new GameObject[LayerUnitNumber];
        centerUnit.transform.localScale = UnitScale * Vector3.one;
        for (int layer = 2; layer <= LayerNumber; layer++) {
            // radius for this layer
            float radius = UnitScale * SqueezeRatio / 2f * layer;
            Debug.Log(radius);
            // create all units
            for (int i = 0; i < LayerUnitNumber; i++) {
                GameObject unit = Instantiate(SlimeUnit, transform);
                unit.transform.localScale = UnitScale * Vector3.one;
                unit.transform.localPosition = radius * GetDirByPropotion((i + layer / 2f) / LayerUnitNumber);

                thisLayerUnits[i] = unit;
            }
            // add joints
            if (layer == 2) {
                for (int i = 0; i < LayerUnitNumber; i++) {
                    AddSpringJointTo(thisLayerUnits[i], centerUnit);
                    AddSpringJointTo(thisLayerUnits[i], thisLayerUnits[(i + 1) % LayerUnitNumber]);
                }
            }
            if (layer >= 3) {
                for (int i = 0; i < LayerUnitNumber; i++) {
                    AddSpringJointTo(thisLayerUnits[i], lastLayerUnits[(i + 1) % LayerUnitNumber]);
                    AddSpringJointTo(thisLayerUnits[i], lastLayerUnits[(i + 1) % LayerUnitNumber]);
                    AddSpringJointTo(thisLayerUnits[i], thisLayerUnits[(i + 1) % LayerUnitNumber]);
                }
            }
            // finish one layer
            for (int i = 0; i < LayerUnitNumber; i++) {
                lastLayerUnits[i] = thisLayerUnits[i];
            }
        }
    }

    private void AddSpringJointTo(GameObject unit, GameObject other, float strength = 30f) {
        if (!unit || !other) {
            Debug.LogAssertion("Unit not exist");
            return;
        }
        if (!unit.GetComponent<Rigidbody2D>() || !other.GetComponent<Rigidbody2D>()) {
            Debug.LogAssertion("No Rigidbody found");
            return;
        }
        SpringJoint2D j = unit.AddComponent<SpringJoint2D>();
        j.connectedBody = other.GetComponent<Rigidbody2D>();
        j.frequency = strength;
        j.autoConfigureDistance = false;
    }

    private Vector2 GetDirByPropotion(float propotion) {
        return (Quaternion.Euler(0f, 0f, propotion * 360f) * Vector2.up).normalized;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
