namespace MapScene
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Utilities;
	using Mapbox.Unity.Map;
	using UnityEngine;

	public class BallManager : MonoBehaviour
	{

		[SerializeField]
		float _positionFollowFactor;

		[SerializeField]
		bool _useTransformLocationProvider;

		bool _isInitialized;

		ILocationProvider _locationProvider;
		public ILocationProvider LocationProvider
		{
			private get
			{
				if (_locationProvider == null)
				{
					_locationProvider = _useTransformLocationProvider ?
						LocationProviderFactory.Instance.TransformLocationProvider : LocationProviderFactory.Instance.DefaultLocationProvider;
				}

				return _locationProvider;
			}
			set
			{
				if (_locationProvider != null)
				{
					_locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;

				}
				_locationProvider = value;
				_locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
			}
		}

		Vector3 _targetPosition;

		void Awake()
		{
			LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
		}

		void OnDestroy()
		{
			if (LocationProvider != null)
			{
				LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
			}
		}

		void LocationProvider_OnLocationUpdated(object sender, LocationUpdatedEventArgs e)
		{
			if (_isInitialized)
			{
				//_targetPosition = Conversions.GeoToWorldPosition(e.Location,
															//	 _map.CenterMercator,
															//	 _map.WorldRelativeScale).ToVector3xz();
			}
		}

		public void OnButtonClicked()
		{
			Debug.Log("Hoooola");
		}

		void Update()
		{
			transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _positionFollowFactor);

			if (Input.GetMouseButtonDown(0))
			{
				//Tires un raig que surt de la càmara en direcció a on tinguis el touch
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				//Creas un RaycasHit què és del que obtindràs tota la informació de la colisió entre el raig i el que s'hagi trobat.
				var hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit))
				{
					Debug.Log("Object Name: "+hit.transform.name);
					Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

					
					//per nom (transform.name) o layer (transform.gameObject.layer)
					// layer es un int 
					if (hit.transform.CompareTag("BallsLayer"))
					{
						Debug.Log("<color=yellow>Floor Hit!</color>");
						//Per saber a quin punt s'ha trobat es tan facil com utilizar el hit.point
						Debug.Log("Floor Point: "+hit.point);
						Debug.Log("my " + hit.transform.name);
					}
				}
			}
		}
	}
}