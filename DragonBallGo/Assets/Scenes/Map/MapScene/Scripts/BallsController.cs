namespace MapScene
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Utilities;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using TMPro;
	using UnityEngine.Networking;
	using UnityEngine.SceneManagement;
	using System.Collections;
	using System.Collections.Generic;
	using System;
    using Mapbox.Utils;
	public class BallsController : MonoBehaviour
	{
		[SerializeField]
		private AbstractMap _map;

		/// <summary>
		/// The rate at which the transform's position tries catch up to the provided location.
		/// </summary>
		[SerializeField]
		float _positionFollowFactor;

		/// <summary>
		/// Use a mock <see cref="T:Mapbox.Unity.Location.TransformLocationProvider"/>,
		/// rather than a <see cref="T:Mapbox.Unity.Location.EditorLocationProvider"/>. 
		/// </summary>
		[SerializeField]
		bool _useTransformLocationProvider;

		bool _isInitialized;

		/// <summary>
		/// The location provider.
		/// This is public so you change which concrete <see cref="T:Mapbox.Unity.Location.ILocationProvider"/> to use at runtime.
		/// </summary>
		ILocationProvider _locationProvider;
		private Balls balls;
		private Balls updatingBalls;
		public GameObject ball1;
		public GameObject ball2;
		public GameObject ball3;
		public GameObject ball4;
		public GameObject ball5;
		public GameObject ball6;
		public GameObject ball7;

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
		IEnumerator ExecuteGet(UnityWebRequest request)
		{
			yield return request.Send();
		
			if (request.isNetworkError)
			{				
				Debug.Log(request.error);
			}
			else
			{
				// Show results as text
				Debug.Log("Returning:" + request.downloadHandler.text);	
				balls = JsonUtility.FromJson<Balls>(request.downloadHandler.text);
			}
			
			UpdateBallsPosition();

			//yield return new WaitForSeconds(5);
			//StartCoroutine(ExecuteGet(Rest.getBallsRequest(PersistentData.getSelectedGameId())));
		}

		IEnumerator ExecutePost()
		{
			UnityWebRequest request = Rest.postBallCatch(getCurrentUpdatingBallsParameters());
			yield return request.Send();

			if (request.isNetworkError)
			{				
				Debug.Log(request.error);
			}
			else
			{
				// Show results as text
				Debug.Log("Returning2:" + request.downloadHandler.text);	
			}

			UpdateBallsPosition();
		}

		void Start()
		{

		}

		void Awake()
		{
			updatingBalls = new Balls();
			updatingBalls.balls = new List<Ball>(7);
			updatingBalls.balls.Add(new Ball());
			updatingBalls.balls.Add(new Ball());
			updatingBalls.balls.Add(new Ball());
			updatingBalls.balls.Add(new Ball());
			updatingBalls.balls.Add(new Ball());
			updatingBalls.balls.Add(new Ball());
			updatingBalls.balls.Add(new Ball());
			
			StartCoroutine(ExecuteGet(Rest.getBallsRequest(PersistentData.getSelectedGameId())));

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
				_targetPosition = Conversions.GeoToWorldPosition(e.Location,
																 _map.CenterMercator,
																 _map.WorldRelativeScale).ToVector3xz();
			}
		}

		void UpdateBallsPosition(){
			if (balls != null)
			{
				Vector3 position = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[0].lat), double.Parse(balls.balls[0].lng)),
																 	_map.CenterMercator,
																 	_map.WorldRelativeScale).ToVector3xz();
				ball1.transform.position = position;


				Vector3 position2 = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[1].lat), double.Parse(balls.balls[1].lng)),
																	_map.CenterMercator,
																	_map.WorldRelativeScale).ToVector3xz();
				ball2.transform.position = position2;

				Vector3 position3 = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[2].lat), double.Parse(balls.balls[2].lng)),
																	_map.CenterMercator,
																	_map.WorldRelativeScale).ToVector3xz();
				ball3.transform.position = position3;

				Vector3 position4 = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[3].lat), double.Parse(balls.balls[3].lng)),
																	_map.CenterMercator,
																	_map.WorldRelativeScale).ToVector3xz();
				ball4.transform.position = position4;

				Vector3 position5 = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[4].lat), double.Parse(balls.balls[4].lng)),
																	_map.CenterMercator,
																	_map.WorldRelativeScale).ToVector3xz();
				ball5.transform.position = position5;

				Vector3 position6 = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[5].lat), double.Parse(balls.balls[5].lng)),
																	_map.CenterMercator,
																	_map.WorldRelativeScale).ToVector3xz();
				ball6.transform.position = position6;

				Vector3 position7 = Conversions.GeoToWorldPosition(new Vector2d(double.Parse(balls.balls[6].lat), double.Parse(balls.balls[6].lng)),
																	_map.CenterMercator,
																	_map.WorldRelativeScale).ToVector3xz();
				ball7.transform.position = position7;
			}
		}

		void addUpdatingBall(Ball ball){
			updatingBalls.balls[int.Parse(ball.ball) - 1] = ball;
		}

		Ball createUpdatingBall(string id, string num, string catched){
			var ball = new Ball();
			ball.ball = num;
			ball.id = id;
			ball.catched = catched;
			ball.user = "laurichu";
			ball.lat = _map.CenterLatitudeLongitude.x.ToString();
			ball.lng = _map.CenterLatitudeLongitude.y.ToString();
			return ball;
		}

		String getCurrentUpdatingBallsParameters()
		{
			return JsonUtility.ToJson(updatingBalls) ?? "";
		}

		void catchedBall(Ball selectedBall){
			var ball1 = createUpdatingBall(selectedBall.id,selectedBall.ball,"1");
			addUpdatingBall(ball1);
			StartCoroutine(ExecutePost());
		}
		void Update()
		{
			//Update Balls and Player Position
			transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _positionFollowFactor);
			//UpdateBallsPosition();

			if (Input.GetMouseButtonDown(0))
			{
				//Tires un raig que surt de la càmara en direcció a on tinguis el touch
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				//Creas un RaycasHit què és del que obtindràs tota la informació de la colisió entre el raig i el que s'hagi trobat.
				var hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit))
				{
					switch (hit.transform.name) {
						case "Ball1":
							Debug.Log("Touched Ball1");
							var selectedBall = balls.balls[0];
							if (selectedBall.catched.Equals("1"))
							{
								// PVP 
								Debug.Log("Touched Ball1 1");
								SceneManager.LoadScene(mScene.BATTLE);
							}
							else
							{
								catchedBall(selectedBall);
							}
							break;
						case "Ball2":
							Debug.Log("Touched Ball2");
							var selectedBall2 = balls.balls[1];
							if (selectedBall2.catched.Equals("1"))
							{
								// PVP 
								Debug.Log("Touched Ball1 2");
								SceneManager.LoadScene(mScene.BATTLE);
							}
							else
							{
								catchedBall(selectedBall2);
							}
							break;
						case "Ball3":
							Debug.Log("Touched Ball3");
							var selectedBall3 = balls.balls[2];
							if (selectedBall3.catched.Equals("1"))
							{
								// PVP 
								Debug.Log("Touched Ball1 3");
								SceneManager.LoadScene(mScene.BATTLE);
							}
							else
							{
								catchedBall(selectedBall3);
							}
							break;
						case "Ball4":
							Debug.Log("Touched Ball4");
							var selectedBall4 = balls.balls[3];
							if (selectedBall4.catched.Equals("1"))
							{
								// PVP 
								Debug.Log("Touched Ball1 4");
								SceneManager.LoadScene(mScene.BATTLE);
							}
							else
							{
								catchedBall(selectedBall4);
							}
							break;
						case "Ball5":
							Debug.Log("Touched Ball5");
							var selectedBall5 = balls.balls[4];
							if (selectedBall5.catched.Equals("1"))
							{
								// PVP 
								Debug.Log("Touched Ball1 5");
								SceneManager.LoadScene(mScene.BATTLE);
							}
							else
							{
								catchedBall(selectedBall5);
							}
							break;
						case "Ball6":
							Debug.Log("Touched Ball6");
							var selectedBall6 = balls.balls[5];
							if (selectedBall6.catched.Equals("1"))
							{
								// PVP 
								Debug.Log("Touched Ball1 6");
								SceneManager.LoadScene(mScene.BATTLE);
							}
							else
							{
								catchedBall(selectedBall6);
							}
							break;
					}
					Debug.Log("Object Name: " + hit.transform.name);
					Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

					
					//per nom (transform.name) o layer (transform.gameObject.layer)
					// layer es un int 
					/*if (hit.transform.CompareTag("Floor"))
					{
						Debug.Log("<color=yellow>Floor Hit!</color>");
						//Per saber a quin punt s'ha trobat es tan facil com utilizar el hit.point
						Debug.Log("Floor Point: "+hit.point);
					}*/
				}
			}
		}

	}
}