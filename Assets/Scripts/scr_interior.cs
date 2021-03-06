﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_interior : MonoBehaviour 
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
	public	scr_place				linked_place					;

[Space(10)][Header("Content")]
	public	List<GameObject>		npc_slots_list					= new List<GameObject>(); 

[Space(10)][Header("References")]
	public	GameObject				npc_shell_prefab				;
	public	GameObject				scene_npc_container				;

	public	GameObject				player_spawn_point_debug		;	// DEBUG

// = = =


// = = = [ MONOBEHAVIOR METHODS ] = = =


// = = =

	void Start () 
	{
		// Set the local "linked_place" variable
		linked_place = GameManager.INT_active_place;

		// Interior generation method
		GenerateInterior(linked_place);

		// Spawn player at the right position
		GameManager.instance.player_reference.SpawnPlayer( player_spawn_point_debug.transform.position );

		return;
	}

// = = = [ CLASS METHODS ] = = =

// = = MAIN GENERATION METHOD = =

	/// <summary>
	/// Generate an interior from the gamemanager's INT_active_place reference.
	/// This method generate every aspects of the interior.
	/// </summary>
	public void GenerateInterior(scr_place place_to_generate)
	{
		Debug.Log("<b>< < < = = = STARTING FULL INTERIOR GENERATION = = = > > ></b>");

		// Launch specific generation methods
		GenerateLayout(place_to_generate);
		GenerateNPCs(place_to_generate);

		Debug.Log("<b>> > > = = = INTERIOR GENERATION COMPLETE = = = < < <</b>");

		return;
	}

// = =

// = = SPECIFIC GENERATION METHODS = =

	/// <summary>
	/// Generate the interior's layout.
	/// </summary>
	public void GenerateLayout(scr_place place_to_generate)
	{
		Debug.Log("<b>= = = Interior's LAYOUT genration = = =</b>");

		return;
	}

	/// <summary>
	/// Generate the interior's npcs actors and place them in the world.
	/// </summary>
	public void GenerateNPCs(scr_place place_to_generate)
	{
		Debug.Log("<b>= = = Interior's NPC genration = = =</b>");

		Debug.Log("Generating <b>" + place_to_generate.place_npcs.Count + "</b> NPCS." );

		// available slots buffer list
		List<GameObject> available_slots = npc_slots_list;

		// spawn npcs
		foreach (var npc in place_to_generate.place_npcs)
		{
			// EMERGENCY STOP METHOD if this loop starts but there no more npc_slot available
			if (npc_slots_list.Count <= 0) { Debug.LogError("ERROR! TRYING TO GENERATE INTERIOR NPC BUT THERE IS NO MORE SLOT AVAILABLE TO SPAWN IT!!"); return; }  

			// draw 1 npc_slot and remove it from the buffer list
			GameObject drawn_slot = npc_slots_list[ Random.Range(0, npc_slots_list.Count - 1) ];
			npc_slots_list.Remove(drawn_slot);

			// spawn npc method
			GameObject instance;
			scr_npc_shell instance_shell_script;

			instance = Instantiate(npc_shell_prefab, drawn_slot.transform.position, Quaternion.identity, scene_npc_container.transform);
			instance_shell_script = instance.GetComponent<scr_npc_shell>();

			instance_shell_script.linked_npc = npc;

			// Update overhead sprite
			instance_shell_script.UpdateQuestOverheadSprite();

		}

		return;
	}

// = =

// = = OTHER METHODS = =

	/// <summary>
	/// Clean the interior and empty it, setting it back to its default content, before generation.
	/// For now, this method can be handled simply by reloading the scene completely.
	/// </summary>
	public void PurgeInterior()
	{	
		Debug.Log("PURGING SCENE: Reloading the interior scene!!!");
		SceneManager.LoadScene("SC_PlaceInteriorDefault");
		return;
	}

// = =


// = = =

}
