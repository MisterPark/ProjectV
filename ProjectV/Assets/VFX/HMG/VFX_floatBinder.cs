using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[VFXBinder("Transform/Ridius")]
public class VFX_floatBinder : VFXBinderBase
{

    [VFXPropertyBinding("System.single")]
    public ExposedProperty FloatProperty;
    
    public Transform Target;
    public override bool IsValid(VisualEffect component)
    {
        return Target != null && component.HasFloat(FloatProperty); 
    }

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetFloat(FloatProperty, Vector3.Distance(transform.position ,Target.position));
    }
}
