using UnityEngine;

public class GunController : MonoBehaviour
{        
    public Transform gun;
    public LineRenderer laserBeam;    

    private float aimSpeed = 0.5f;
    private float maxDistance = 100f;
    public int maxReflections;


    void Update()
    {
        TurretAim();
        LaserEmitting();
    }

    private void TurretAim()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, aimSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(0, 0, -aimSpeed);
        }
    }

    private void LaserEmitting()
    {
        Ray2D ray = new Ray2D(gun.position, transform.right);

        laserBeam.positionCount = 1;
        laserBeam.SetPosition(0, gun.position);
        float rayLength = maxDistance;

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength);
            if (hit)
            {
                laserBeam.positionCount += 1;
                laserBeam.SetPosition(laserBeam.positionCount - 1, hit.point);
                rayLength -= Vector2.Distance(ray.origin, hit.point);
                ray = new Ray2D(hit.point + hit.normal * 0.001f, Vector2.Reflect(ray.direction, hit.normal));                
            }
            else
            {
                laserBeam.positionCount += 1;
                laserBeam.SetPosition(laserBeam.positionCount - 1, ray.origin + ray.direction * rayLength);
            }
        }
    }
}
