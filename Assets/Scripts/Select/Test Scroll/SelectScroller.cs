using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class SelectScroller : MonoBehaviour
{
    [SerializeField]
    private UIVerticalScroller vScroller = null;
    
    public void Initialize()
    {
        
    }

    private void Update()
    {
        vScroller.UpdateInput();
    }
}
