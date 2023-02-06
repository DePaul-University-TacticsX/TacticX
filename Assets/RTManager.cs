using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RTManager : MonoBehaviour
{

    public LinkedList<CharacterEntity> entities;
    public CharacterEntity active_component;
    public int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        this.entities = new LinkedList<CharacterEntity>();
        this.entities.AddFirst(new Warrior(0.5f)); // Slow Speed
        this.active_component = this.entities.First.Value;


    }

    void next_in_line() {

        if (index == this.entities.Count)
        {

            index = 0;

        }
        else {

            index = ++index;
            
        }

        this.active_component = this.entities.ElementAt(this.index);

    }

    public CharacterEntity getActive() {

        return this.active_component;

    }
}
