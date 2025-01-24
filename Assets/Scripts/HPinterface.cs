using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPinterface : MonoBehaviour
{
    public Image imHP1;
    public Image imHP2;
    public Image imHP3;
    public Image imHP4;
    public Image imHP5;
    public Image imHP6;
    public Image imHP7;
    public Image imHP8;
    public Image imHP9;
    public Image imHP10;
    public Sprite fullH;
    public Sprite emptyH;
    // Start is called before the first frame update
    public void UpdateHealBar(int hp)
    {
        if (hp == 0)
        {
            imHP1.sprite = emptyH;
            imHP2.sprite = emptyH;
            imHP3.sprite = emptyH;
            imHP4.sprite = emptyH;
            imHP5.sprite = emptyH;
            imHP6.sprite = emptyH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 1)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = emptyH;
            imHP3.sprite = emptyH;
            imHP4.sprite = emptyH;
            imHP5.sprite = emptyH;   
            imHP6.sprite = emptyH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;    
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if(hp == 2)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = emptyH;
            imHP4.sprite = emptyH;
            imHP5.sprite = emptyH;
            imHP6.sprite = emptyH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 3)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = emptyH;
            imHP5.sprite = emptyH;
            imHP6.sprite = emptyH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 4)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = emptyH;
            imHP6.sprite = emptyH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 5)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = fullH;
            imHP6.sprite = emptyH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 6)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = fullH;
            imHP6.sprite = fullH;
            imHP7.sprite = emptyH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 7)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = fullH;
            imHP6.sprite = fullH;
            imHP7.sprite = fullH;
            imHP8.sprite = emptyH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 8)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;  
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = fullH;
            imHP6.sprite = fullH;
            imHP7.sprite = fullH;
            imHP8.sprite = fullH;
            imHP9.sprite = emptyH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 9)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = fullH;        
            imHP6.sprite = fullH;
            imHP7.sprite = fullH;
            imHP8.sprite = fullH;
            imHP9.sprite = fullH;
            imHP10.sprite = emptyH;
        }
        else if (hp == 10)
        {
            imHP1.sprite = fullH;
            imHP2.sprite = fullH;
            imHP3.sprite = fullH;
            imHP4.sprite = fullH;
            imHP5.sprite = fullH;
            imHP6.sprite = fullH;
            imHP7.sprite = fullH;
            imHP8.sprite = fullH;
            imHP9.sprite = fullH;
            imHP10.sprite = fullH;
        }
          
            
        
    }
}
