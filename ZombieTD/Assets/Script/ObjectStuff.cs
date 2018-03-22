using UnityEngine;
[RequireComponent (typeof(BoxCollider2D))]
public class ObjectStuff: MonoBehaviour
{
    public LayerMask collisionMask;
    const float skinWidth = 0.015f;
    public int horizontalRayCount = 2;
    public int verticalRayCount = 2;
    float horizontalRaySpacing;
    float verticalRaySpacing;
    new BoxCollider2D collider;
    public CollisionInfo collisions;
    RaycastOrigins raycastOrigins;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }
    public void Move(Vector3 velocity)
    {
        collisions.reset();
        UpdateRaycastOrigins();
        if (velocity.x != 0)
            HorisontalCollisions(ref velocity);
        if (velocity.y != 0)
            VerticalCollisions(ref velocity);
        transform.Translate(velocity);
    }
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    } 
    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }
    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y)+ skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            if (hit)
            {
                velocity.y = (hit.distance - skinWidth)* directionY;
                rayLength = hit.distance;
                collisions.bellow = directionY == -1;
                collisions.above = directionY == 1;
            }
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
        }
    }
    void HorisontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i );
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
            Debug.DrawRay(rayOrigin, Vector2.right * directionX* rayLength, Color.red);
        }
    }
    public struct CollisionInfo
    {
        public bool above, bellow;
        public bool left, right;
        public void reset()
        {
            above = bellow = false;
            left = right = false;
        }
    }
}
public interface Ikillable
{
    void TakeDamage(int damage,float direction);
    void Die();
}

