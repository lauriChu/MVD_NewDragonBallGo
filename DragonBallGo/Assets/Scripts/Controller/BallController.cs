using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using UnityEngine;
public class BallController : MonoBehaviour {

	[SerializeField]
		private AbstractMap _map;

		// Balls
		private GameObject[] _balls;

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
		Vector3[] _targetPositions;
		Vector2d _location;
		Vector2d[] _locations;
		void Start()
		{
			
			_balls = new GameObject[7];
			_targetPositions = new Vector3[7];
			_location = new Vector2d(41.408065, 2.1301014);
			//_location = new Vector2d(37.784328, -122.40364);
			_locations = new Vector2d[7];
			/*for (int i = 0; i < 7; i++){
				_balls[i] = GameObject.Find("Ball" + (i+1).ToString());
				//_locations[i] = BallUtilities.GetRandomPositionFrom((float)_location.x, (float)_location.y, 0.005f);

				_locations[i] = new Vector2d(_location.x + Random.Range(-0.001f, 0.001f), _location.y + Random.Range(-0.001f, 0.001f));
			}*/					 
		}

		void Awake()
		{
			LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
			_map.OnInitialized += () => _isInitialized = true;
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
				//_location = new Vector2d(e.Location.x + 0.0005, e.Location.y + 0.0005);
				_location = new Vector2d(38.9,-122.40364);
				//BallUtilities.GetRandomPositionFrom((float)e.Location.x, (float)e.Location.y, 50f);
				
				/*for (int i = 0; i < 7; i++){
					_targetPosition = Conversions.GeoToWorldPosition(_locations[i],
												_map.CenterMercator,
											 	_map.WorldRelativeScale).ToVector3xz();
					_targetPosition.y = 4;
					_balls[i].transform.position = _targetPosition;
				}

				GameObject.Find("Ball2").transform.position = _targetPosition;*/

				//Vector2d mLocation = new Vector2d(37.7,-122.40364);
				//_isFirst = false;
				//for (int i = 0; i < 7; i++){
					//Vector2d mLocation = BallUtilities.GetRandomPositionFrom((float)e.Location.x, (float)e.Location.y, 50f);
				//	_targetPositions[i] = Conversions.GeoToWorldPosition(mLocation,
																 	//_map.CenterMercator,
																 	//_map.WorldRelativeScale).ToVector3xz();
					//_balls[i].transform.position = _targetPositions[i];
				//}
				//if (_location == null)
				//{
					//_location = BallUtilities.GetRandomPositionFrom((float)e.Location.x, (float)e.Location.y, 0.005f);
					//_location = new Vector2d(e.Location.x + 0.0005, e.Location.y + 0.0005);
				//}
				/*_targetPosition = Conversions.GeoToWorldPosition(_location,
												_map.CenterMercator,
											 	_map.WorldRelativeScale).ToVector3xz();*/
				//GameObject.Find("Ball2").transform.position = _targetPosition;
				//transform.position = _targetPosition;
			
			}
		}

		void Update()
		{
			for (int i = 0; i < Input.touchCount; ++i)
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					foreach (var ball in _balls)
					{
						Rect rect = new Rect(ball.transform.position.x, ball.transform.position.z, 200, 200);
						if (rect.Contains(Input.GetTouch(i).position)){
							Debug.Log("Inside");
						}else{
							Debug.Log("Outside");
						}
					}
					
					
				}
			}

			
			//transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _positionFollowFactor);
			//if (_isSecond){
				/*for(int i = 0; i < 7; i++)
				{
					_balls[i].transform.position = Vector3.Lerp(transform.position, _targetPositions[i], Time.deltaTime * _positionFollowFactor);
				}
				_isSecond = false;*/
				//GameObject.Find("Ball2").transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _positionFollowFactor);
			//}
			//transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _positionFollowFactor);
		}

		void OnCollisionEnter(Collision collision)
		{
			foreach (ContactPoint contact in collision.contacts)
			{
				Debug.DrawRay(contact.point, contact.normal, Color.white);
				
			}
			Debug.Log("touched");
				
		}
}


	
