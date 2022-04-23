using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdater
{
    abstract void UpdateEx();
}

public interface IFixedUpdater
{
    abstract void FixedUpdateEx();
}

public interface ILateUpdater
{
    abstract void LateUpdateEx();
}
