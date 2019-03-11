using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audience.game
{

    public abstract class APanelManager : MonoBehaviour
    {

        protected void PollEscapeKey()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitScreen();
            }
        }

        public virtual void ExitScreen()
        {
            Destroy(gameObject);
        }

    }

}
