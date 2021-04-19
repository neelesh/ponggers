using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
	public List<GameObject> spawnPoints;
	public List<GameObject> powerUps;

	public Boundary ceiling;
	public Boundary floor;

	public float secondsDelay = 3f;
	public bool waiting = false;

	void Update()
	{
		if (!waiting) StartCoroutine(SpawnPowerUp());
	}

	IEnumerator SpawnPowerUp()
	{
		waiting = true;
		yield return new WaitForSeconds(secondsDelay);
		GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];
		GameObject powerUp;

		powerUp = PickPowerUp();
		if (powerUp != null)
		{
			powerUp.transform.position = spawnPoint.transform.position;
			powerUp.SetActive(true);
		}
		waiting = false;
	}

	public GameObject PickPowerUp()
	{
		if (powerUps.Count == 0) return null;
		GameObject powerUp = powerUps[Random.Range(0, powerUps.Count - 1)];

		if (powerUp.GetComponent<Powerup>().topLeftAvantage && ceiling.leftAdvantage) PickPowerUp();
		if (powerUp.GetComponent<Powerup>().topRightAvantage && ceiling.rightAdvantage) PickPowerUp();
		if (powerUp.GetComponent<Powerup>().bottomLeftAvantage && floor.leftAdvantage) PickPowerUp();
		if (powerUp.GetComponent<Powerup>().bottomRightAvantage && floor.rightAdvantage) PickPowerUp();

		powerUps.Remove(powerUp);
		return powerUp;
	}


	public void Recycle(GameObject PowerUpGO)
	{
		PowerUpGO.GetComponent<Powerup>().Reset();
		PowerUpGO.SetActive(false);
		powerUps.Add(PowerUpGO);
	}
}
