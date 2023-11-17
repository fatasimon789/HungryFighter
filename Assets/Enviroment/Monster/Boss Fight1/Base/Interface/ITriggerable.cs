using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable 
{
   bool IsAgroed { get; set; }
   bool IsWithStrikingDistance { get; set; }

    void SetAgroStatus(bool isAggroed);

    void SetStrikingDistanceBool(bool isWithStringkingDistance);
}
