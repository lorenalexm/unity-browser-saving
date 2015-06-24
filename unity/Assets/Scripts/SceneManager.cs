using UnityEngine;

public class SceneManager : MonoBehaviour {

	//-------------------------------------------------
	#region Fields

	[SerializeField]
	private Transform[] cubes;
	[SerializeField]
	private float range = 5.0f;
	[SerializeField]
	private Transform cameraPivot = null;
	[SerializeField]
	private float rotationSpeed = 5.0f;

	#endregion


	//-------------------------------------------------
	#region Start method

	private void Start() {
		if (this.cubes.Length != 0) {
			for (int i = 0; i < this.cubes.Length; i++) {
				cubes [i].position = new Vector3 (Random.Range (-this.range, this.range), Random.Range (-this.range, this.range), Random.Range (-this.range, this.range));
				cubes [i].eulerAngles = new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
				SceneSerializer.AddCube(cubes[i]);
			}
		}
	}

	#endregion


	//-------------------------------------------------
	#region Update method

	private void Update()
	{
		if (this.cameraPivot != null) {
			this.cameraPivot.Rotate (0, this.rotationSpeed * Time.deltaTime, 0);
		}
	}

	#endregion


	//-------------------------------------------------
	#region OnRandomizeClicked method

	public void OnRandomizeClicked() {
		if (this.cubes.Length != 0) {
			SceneSerializer.Collection.cubes.Clear();
			for (int i = 0; i < this.cubes.Length; i++) {
				cubes [i].position = new Vector3 (Random.Range (-this.range, this.range), Random.Range (-this.range, this.range), Random.Range (-this.range, this.range));
				cubes [i].eulerAngles = new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
				SceneSerializer.AddCube(cubes[i]);
			}
		}
	}

	#endregion


	//-------------------------------------------------
	#region OnSaveClicked method

	public void OnSaveClicked() {
		string serializedData = SceneSerializer.Serialize ();
		Application.ExternalCall ("saveSceneToDisk", serializedData);
	}

	#endregion


	//-------------------------------------------------
	#region OnReceiveData method
	
	public void OnReceiveData(string data) {
		if (SceneSerializer.Deserialize (data)) {
			if (this.cubes.Length != 0) {
				Vector3 vector = new Vector3();
				for (int i = 0; i < this.cubes.Length; i++) {
					vector.x = SceneSerializer.Collection.cubes[i].posX;
					vector.y = SceneSerializer.Collection.cubes[i].posY;
					vector.z = SceneSerializer.Collection.cubes[i].posZ;
					cubes [i].position = vector;
					vector.x = SceneSerializer.Collection.cubes[i].rotX;
					vector.y = SceneSerializer.Collection.cubes[i].rotY;
					vector.z = SceneSerializer.Collection.cubes[i].rotZ;
					cubes [i].eulerAngles = vector;
				}
			}
		}
	}
	
	#endregion

}
