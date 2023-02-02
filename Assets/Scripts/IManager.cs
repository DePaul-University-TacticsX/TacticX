using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager {
  ManagerStatus GetStatus();
  void StartUp();    // handle initialization of the individual manager
}
