using UnityEngine;

namespace DefaultNamespace
{ 
    //Атрибут метка . сам по себе ничего не делает:
    //он только помечает поле , а как его рисовать решает ReadOnlyDrawer ( папка Editor)

    public class ReadOnlyAttribute : PropertyAttribute
    
    {

    }
    
}