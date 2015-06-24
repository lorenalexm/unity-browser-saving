using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class CubeData {

	//-------------------------------------------------
	#region Fields

	[XmlAttribute("PositionX")]
	public float posX;
	[XmlAttribute("PositionY")]
	public float posY;
	[XmlAttribute("PositionZ")]
	public float posZ;
	[XmlAttribute("RotationX")]
	public float rotX;
	[XmlAttribute("RotationY")]
	public float rotY;
	[XmlAttribute("RotationZ")]
	public float rotZ;

	#endregion

}

[XmlRoot("Scene")]
public class SceneCollection {

	//-------------------------------------------------
	#region Fields

	[XmlArray("Cubes"), XmlArrayItem("Cube")]
	public List<CubeData> cubes = new List<CubeData>();

	#endregion

}

public static class SceneSerializer {

	//-------------------------------------------------
	#region Fields

	private static SceneCollection collection = new SceneCollection();

	#endregion


	//-------------------------------------------------
	#region Properties

	public static SceneCollection Collection {
		get {
			if(collection != null) {
				return collection;
			}

			Debug.LogError("Unable to access collection object.");
			return null;
		}
	}

	#endregion


	//-------------------------------------------------
	#region AddCube method

	public static void AddCube(Transform cube) {
		if (cube != null) {
			CubeData data = new CubeData();
			data.posX = cube.position.x;
			data.posY = cube.position.y;
			data.posZ = cube.position.z;
			data.rotX = cube.eulerAngles.x;
			data.rotY = cube.eulerAngles.y;
			data.rotZ = cube.eulerAngles.z;
			collection.cubes.Add(data);
		}
	}

	#endregion


	//-------------------------------------------------
	#region Serialize method

	public static string Serialize() {
		XmlSerializer xmls = new XmlSerializer(typeof(SceneCollection));
		using(StringWriter writer = new StringWriter()) {
			xmls.Serialize(writer, collection);
			return writer.ToString();
		}
	}

	#endregion


	//-------------------------------------------------
	#region Deserialize method

	public static bool Deserialize(string data) {
		StringReader reader = null;
		XmlSerializer xmls = new XmlSerializer(typeof(SceneCollection));

		try {
			reader = new StringReader(data);
			collection = xmls.Deserialize(reader) as SceneCollection;
		}
		catch(Exception e) {
			Debug.LogError("Something went horribly wrong: \n" + e.ToString());
			return false;
		}
		finally {
			reader.Dispose();
		}

		return true;
	}

	#endregion

}
