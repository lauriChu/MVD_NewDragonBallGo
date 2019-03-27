using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;

public class BallUtilities : MonoBehaviour {

	//Unless you are within 50 km of a pole, your square around (lon αα, lat ββ) should extend to 
	//approximately β±50111β±50111 degrees (because one degree is roughly 111 km) in latitude, and 
	//to approximately α±50111cosβα±50111cos⁡β in longitude. (It appears that you arriverd at your 
	//formulas by considering a place at about 50∘50∘ N or S).
	public static Vector2d GetRandomPositionFrom(float lat, float lng, float kmR)
	{
		float maxlatitude = lat + kmR / 111;
		float minlatitude = lat - kmR / 111;
		float maxlongitude = lng + kmR / (111 * (float)Mathd.Cos(lat));
		float minlongitude = lng + kmR / (111 * (float)Mathd.Cos(lat));

		double mlat = GetRandomNumber(minlatitude, maxlatitude);
		double mlng = GetRandomNumber(minlongitude, maxlongitude);

		return new Vector2d(mlat, mlng);
	}

	public static double GetRandomNumber(float minimum, float maximum)
	{ 
		return Random.Range(minimum, maximum);
	}
}
