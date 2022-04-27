using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Grid
{
    /// Responsible for choosing the coordinates in which a slot can be instantiated.
    public class GridManager : MonoBehaviour
    {
        [SerializeField] float bottomScreenBorder = 0.05f;
        [SerializeField] float upperScreenBorder = 0.85f;
        [SerializeField] float rightScreenBorder = 0.97f;
        [SerializeField] float leftScreenBorder = 0.03f;


        public Vector3 GetPopulationCoordinate(int zPos)
        {
            Vector2 coordinatesV2 = GetXYCoordinates();
            // Turns the random vector2 to vector3 according to some logic (Z = i)
            Vector3 coordinates = new Vector3(coordinatesV2.x, coordinatesV2.y, zPos);
            return coordinates;        
        }

        private Vector2 GetXYCoordinates()
        {
            // Creates viewport random X, Y
            float randX = Random.Range(leftScreenBorder, rightScreenBorder);
            float randY = Random.Range(bottomScreenBorder, upperScreenBorder);
            // Turns X, Y to Vector with Z = 0
            Vector3 randScreenPoint = new Vector3(randX, randY, 0);
            randScreenPoint = Camera.main.ViewportToWorldPoint(randScreenPoint);
            // Extracts only screenview X, Y coordinates
            Vector2 coordinatesV2 = new Vector2(randScreenPoint.x, randScreenPoint.y);
            return coordinatesV2;
        }

    }
}

