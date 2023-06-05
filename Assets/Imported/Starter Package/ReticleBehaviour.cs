﻿/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ARPlane CurrentPlane;

    // Start is called before the first frame update
    private void Start()
    {
        Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        // Conduct a ray cast to position this object.
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        var hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;
        // If raycast hits something
        if (hits.Count > 0)
        {
            // Check if locked plane is null. If so, take the first hit surface instead. If not then take the locked plane
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = (lockedPlane == null) ? hits[0] : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }

        // If there is a hit, them set the hit plane to the CurrentPlane and set the transform of the reticle to the hit position
        if (hit.HasValue)
        {
            CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
            transform.position = hit.Value.pose.position;
        }
        Child.SetActive(CurrentPlane != null);
    }
}
