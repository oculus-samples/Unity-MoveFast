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

using Unity.Collections;
using UnityEngine;

/// <summary>
/// Generates a mesh that represents a plane's boundary.
/// </summary>
/// <remarks>
/// When added to a GameObject that represents a scene entity, such as a floor, ceiling, or desk, this component
/// generates a mesh from its boundary vertices.
/// </remarks>
[RequireComponent(typeof(MeshFilter))]
public class OVRScenePlaneMeshFilter : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.sharedMesh = _mesh;

        CreateMeshFromBoundary();
    }

    private void CreateMeshFromBoundary()
    {
        var sceneAnchor = GetComponent<OVRSceneAnchor>();
        if (sceneAnchor == null) return;

        _mesh.name = $"OVRPlaneMeshFilter {sceneAnchor.Uuid}";

        var boundary = OVRPlugin.GetSpaceBoundary2D(sceneAnchor.Space, Allocator.Temp);
        if (!boundary.IsCreated) return;

        using (boundary)
        {
            OVRMeshGenerator.GenerateMesh(boundary, _mesh);
        }
    }
}
