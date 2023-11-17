using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveable
{
    Rigidbody2D RB { get; set; }

    bool isFacingRight { get; set; }

    void EnemyAction(Vector2 velocity);
    void checkForLeftOrRightFacing(Vector2 velocity);
}
