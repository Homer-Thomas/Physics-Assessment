using UnityEngine;

public class Firing : MonoBehaviour
{
    public float angle; // Angle in degrees
    public float speed; // Speed in m/s
    public float initialHeight = 1.0f; // Initial height in meters
    public float mass = 1.0f; // Mass of the ball in kg
    private float g = 9.81f; // Acceleration due to gravity (m/s^2)
    private float airDensity = 1.225f; // Air density at sea level (kg/m^3)
    private float dragCoefficient = 0.47f; // Drag coefficient for a sphere
    private float radius = 0.1f; // Radius of the ball in meters
    private float area; // Cross-sectional area of the ball

    void Start()
    {
        area = Mathf.PI * radius * radius; // Calculate cross-sectional area
        // Calculate the distances and air time
        float rangeDistance, arcDistance, airTime;
        CalculateDistances(angle, speed, initialHeight, out rangeDistance, out arcDistance, out airTime);
        Debug.Log($"Range Distance: {rangeDistance} meters");
        Debug.Log($"Arc Distance: {arcDistance} meters");
        Debug.Log($"Air Time: {airTime} seconds");
    }

    void CalculateDistances(float angle, float speed, float initialHeight, out float rangeDistance, out float arcDistance, out float airTime)
    {
        // Convert angle to radians
        float angleRad = angle * Mathf.Deg2Rad;

        // Initial velocity components
        float vX = speed * Mathf.Cos(angleRad);
        float vY = speed * Mathf.Sin(angleRad);

        // Time of flight (considering initial height)
        airTime = (vY + Mathf.Sqrt(vY * vY + 2 * g * initialHeight)) / g;

        // Horizontal distance (range)
        rangeDistance = vX * airTime;

        // Calculate arc distance using numerical integration
        int steps = 1000; // Number of steps for numerical integration
        float deltaTime = airTime / steps;
        arcDistance = 0f;
        Vector3 previousPosition = new Vector3(0, initialHeight, 0);

        for (int i = 1; i <= steps; i++)

        {
            float t = i * deltaTime;
            float x = vX * t;
            float y = initialHeight + vY * t - 0.5f * g * t * t;
            Vector3 currentPosition = new Vector3(x, y, 0);

            if (i > 1)
            {
                arcDistance += Vector3.Distance(previousPosition, currentPosition);
            }

            previousPosition = currentPosition;
        }
    }
}