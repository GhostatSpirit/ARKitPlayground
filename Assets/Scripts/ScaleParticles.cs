﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScaleParticles : MonoBehaviour
{
	// @zpj default scale size;
	public float ScaleSize = 1.0f;
	private List<float> initialSizes = new List<float>();

	void Awake()
	{
		// Save off all the initial scale values at start.
		ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particle in particles)
		{
			initialSizes.Add(particle.main.startSize.constant);
			ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
			if (renderer)
			{
				initialSizes.Add(renderer.lengthScale);
				initialSizes.Add(renderer.velocityScale);
			}
		}
	}

	void Start()
	{
		gameObject.transform.localScale = new Vector3(ScaleSize, ScaleSize, ScaleSize);
		// Scale all the particle components based on parent.
		int arrayIndex = 0;
		ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particle in particles)
		{
			float tempSize = initialSizes[arrayIndex++] * gameObject.transform.localScale.magnitude;
			var main = particle.main;
			main.startSize = tempSize;
			ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
			if (renderer)
			{
				renderer.lengthScale = initialSizes[arrayIndex++] *
					gameObject.transform.localScale.magnitude;
				renderer.velocityScale = initialSizes[arrayIndex++] *
					gameObject.transform.localScale.magnitude;
			}
		}
	}

}