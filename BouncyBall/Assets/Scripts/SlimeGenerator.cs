using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour {

    public int OutLayerNumber = 2;
    public int StartLayerUnitNumber = 4;
    public float StartRadius = 0.2f;

    [SerializeField] GameObject SlimeUnit;

	// Use this for initialization
	void Start () {
        GenerateSlime();
	}

    private void GenerateSlime() {
        GameObject centerUnit = Instantiate(SlimeUnit, transform.position, transform.rotation, transform);
        List<GameObject> lastLayerUnits = new List<GameObject>();
        List<GameObject> thisLayerUnits = new List<GameObject>();
        // initial value
        int unitNum = StartLayerUnitNumber;
        float radius = StartRadius;
        for (int layer = 1; layer <= OutLayerNumber; layer++) {
            // create all units
            for (int i = 0; i < unitNum; i++) {
                GameObject unit = Instantiate(SlimeUnit, transform);
                unit.transform.localPosition = radius * GetDirByPropotion(i / (float) unitNum);
                thisLayerUnits.Add(unit);
            }
            // add joints
            if (layer == 1) {
                for (int i = 0; i < unitNum; i++) {
                    AddSpringJointTo(thisLayerUnits[i], thisLayerUnits[(i + 1) % unitNum]);
                    AddSpringJointTo(thisLayerUnits[i], centerUnit);
                }
            }
            if (layer >= 2) {
                for (int i = 0; i < unitNum; i++) {
                    AddSpringJointTo(thisLayerUnits[i], thisLayerUnits[(i + 1) % unitNum]);
                    if (i % 2 == 0) {
                        AddSpringJointTo(thisLayerUnits[i], lastLayerUnits[(i / 2) % (unitNum / 2)]);
                    } else {
                        AddSpringJointTo(thisLayerUnits[i], lastLayerUnits[(i + 1) / 2 % (unitNum / 2)]);
                        AddSpringJointTo(thisLayerUnits[i], lastLayerUnits[(i - 1) / 2 % (unitNum / 2)]);
                    }
                }
            }
            // finish one layer
            lastLayerUnits.Clear();
            lastLayerUnits.AddRange(thisLayerUnits);
            thisLayerUnits.Clear();
            unitNum *= 2;
            radius += StartRadius;
        }
    }

    private void AddSpringJointTo(GameObject unit, GameObject other, float strength = 5f) {
        if (!unit || !other) {
            Debug.LogAssertion("Unit not exist");
            return;
        }
        if (!unit.GetComponent<Rigidbody2D>() || !other.GetComponent<Rigidbody2D>()) {
            Debug.LogAssertion("No Rigidbody found");
            return;
        }
        SpringJoint2D j = unit.AddComponent<SpringJoint2D>();
        j.autoConfigureConnectedAnchor = true;
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
