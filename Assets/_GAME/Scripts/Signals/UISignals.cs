using Project.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Signals
{
    public class UISignals
    {
        public static UnityAction<TextNotifyArgs> OnTextPopUpRequest = delegate { };
        public static UnityAction OnPopUpCloseRequest = delegate { };
    }
}