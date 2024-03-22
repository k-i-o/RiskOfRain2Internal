using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RiskOfRain2Internal
{
    public class GUIHelper
	{ 
		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

		public static Color Color
		{
			get { return GUI.color; }
			set { GUI.color = value; }
		}

		public static void DrawString(Vector2 position, string label, bool centered = true, bool alignTopLeft = false)
		{
			/*var content = new GUIContent(label);
			var size = StringStyle.CalcSize(content);
			var upperLeft = centered ? position - size / 2f : position;
			GUI.Label(new Rect(upperLeft, size), content);*/
			GUIStyle style = GUI.skin.label; // Utilizza lo stile di default per le etichette GUI
			GUIContent content = new GUIContent(label);
			Vector2 size = style.CalcSize(content);

			// Se il testo deve essere centrato, calcola la posizione in modo che il centro del testo sia a "position"
			if (centered)
			{
				position.x -= size.x / 2f;
				position.y -= size.y / 2f;
			}
			// Se il testo deve essere allineato in alto a sinistra, non effettua alcun aggiustamento
			else if (!alignTopLeft)
			{
				position.x -= size.x / 2f;
				position.y -= size.y / 2f;
			}

			GUI.Label(new Rect(position.x, position.y, size.x, size.y), content, style);

		}

		private static Texture2D lineTex;
		private static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width) // problem with mirroring
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!lineTex)
				lineTex = new Texture2D(1, 1);

			Color color2 = GUI.color;
			GUI.color = color;

			float num = Vector3.Angle(pointB - pointA, Vector2.right);

			if (pointA.y > pointB.y)
				num = -num;

			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		private static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
		{
			DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
		}

		public static void DrawESPLine(Vector3 point, Color color, float thickness)
        {
			Vector3 pointInScreen = Camera.main.WorldToScreenPoint(point);

			DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(pointInScreen.x, (float)Screen.height - pointInScreen.y), color, thickness);
		}

		public static void DrawESPBox(Vector3 position, Color color, float thickness)
        {
			Vector3 playerFootPos = new Vector3(position.x, position.y - thickness, position.z);
			Vector3 playerHeadPos = new Vector3(position.x, position.y + thickness, position.z);

			Vector3 footPointInScreen = Camera.main.WorldToScreenPoint(playerFootPos);
			Vector3 headPointInScreen = Camera.main.WorldToScreenPoint(playerHeadPos);

			float height = headPointInScreen.y - footPointInScreen.y;
			float width = height / thickness;

			DrawBox(footPointInScreen.x - (width / 2), (float)Screen.height - footPointInScreen.y - height, width, height, color, thickness);
		}

		public static void DrawESPNameTag(Vector3 position, string text)
		{
			Vector3 playerHeadPos = new Vector3(position.x, position.y + 2f, position.z);

			Vector3 headPointInScreen = Camera.main.WorldToScreenPoint(playerHeadPos);

			DrawString(new Vector2(headPointInScreen.x, headPointInScreen.y - 10), text);
		}
	}
}
