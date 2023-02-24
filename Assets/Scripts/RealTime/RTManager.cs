using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RTManager : MonoBehaviour
{

    public LinkedList<CharacterEntity> entities;
    private Vector3 c_position = new Vector3(0f, 2f, -10f);
    public CharacterEntity active_component;
    public int index = 0;
    private static RTManager instance;


    // Start is called before the first frame update
    void Start()
    {

        // set up a listener on the event that a character was attacked

        instance = this;
        this.entities = new LinkedList<CharacterEntity>();

        CharacterEntity warrior_char = new Warrior(2.0f);
        CharacterEntity archer_char = new Archer(5.0f);
        CharacterEntity mage_char = new Mage(7.0f);

        warrior_char.entity.SetActive(false);
        archer_char.entity.SetActive(false);
        mage_char.entity.SetActive(false);

        this.entities.AddFirst(warrior_char); // Slow Speed
        this.entities.AddLast(archer_char); // Medium Speed
        this.entities.AddLast(mage_char); // Fast Speed

        this.active_component = this.entities.First.Value;
        this.active_component.entity.transform.position = c_position;
        Camera.main.transform.SetParent(instance.active_component.entity.transform);

        RTManager.ActivateEntity(); // The head of the list is initially seen as activated.
        
  
    }

    public static void next_in_line() {

       CharacterEntity last_active = instance.active_component;

        if (instance.index == instance.entities.Count-1)
        {

            instance.index = 0;

        }
        else {

            instance.index = ++instance.index;
            
        }

        // Next element in list.
        instance.active_component = instance.entities.ElementAt(instance.index);

        // update camera to follow new object
        // we update the camera's position to keep following the active entity's position,
        // and assign the active component as the parent to the new activated object
        // this prevents camera offsets, before the camera would always use the value from the previously
        // activated object 
        instance.active_component.entity.transform.SetPositionAndRotation(last_active.get_position(),
    last_active.get_rotation());
        Camera.main.transform.SetParent(instance.active_component.entity.transform);

        // The Active Component shall now be activated, and the last active component will be deactivated.
        RTManager.DeactivateEntity(last_active);
        RTManager.ActivateEntity();



    }

    public static CharacterEntity getActive() {

        return instance.active_component;

    }

    public static void ActivateEntity() {

        instance.active_component.entity.SetActive(true);

    }

    public static void DeactivateEntity(CharacterEntity node) {

        node.entity.SetActive(false);

    }

    public static void DecreaseHealth(CharacterEntity player, int amount) {
      player.minus_health(amount);
    }

    
}
