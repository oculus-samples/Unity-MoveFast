/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;

public class GazeTargetSpawner : MonoBehaviour
{
    public GameObject GazeTargetPrefab;
    public int NumberOfDummyTargets = 100;
    public int RadiusMultiplier = 3;
    [SerializeField]
    private bool isVisible;
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        set
        {
            isVisible = value;
            GazeTarget[] dummyGazeTargets = gameObject.GetComponentsInChildren<GazeTarget>();
            for (int i = 0; i < dummyGazeTargets.Length; ++i)
            {
                MeshRenderer dummyMesh = dummyGazeTargets[i].GetComponent<MeshRenderer>();
                if (dummyMesh != null)
                {
                    dummyMesh.enabled = isVisible;
                }
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < NumberOfDummyTargets; ++i)
        {
            GameObject target = Instantiate(GazeTargetPrefab, transform);
            target.name += "_" + i;
            target.transform.localPosition = Random.insideUnitSphere * RadiusMultiplier;
            target.transform.rotation = Quaternion.identity;
            target.GetComponent<MeshRenderer>().enabled = IsVisible;
        }
    }

    void OnValidate()
    {
        // Run through OnValidate to pick up changes from inspector
        IsVisible = isVisible;
    }
}
