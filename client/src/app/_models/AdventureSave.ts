import { ActionTrigger } from "./actionTrigger";
import { Item } from "./item";
import { NPC } from "./npc";
import { PlayerCharacter } from "./playerCharacters/playerCharacter";

export interface Adventure{
    id: number;
    name: string;
    saveDescription: string;
    description : string;
    currentLocation : AdventureLocation;
    locations: AdventureLocation[];
    playerCharacters: PlayerCharacter[];
}

export interface AdventureLocation{
    id: number;
    locationId: number;
    name: string;
    description : string;
    npCs: NPC[],
    containers: Container[]
    connectedLocations: AdventureLocation[]
    interactions: Interaction[]
    triggers: ActionTrigger[];
    enemies: Enemy[],
}

export interface Container{
    id: number;
    name: string;
    description: string;
    items: Item[];
}

export interface Interaction{
    id: number;
    name: string;
    information: string;
    locationId: number;
    triggers: ActionTrigger[];
}

export interface Enemy{
    id: number;
    name: string;
    photoUrl: string;
    maxHp: number;
    currentHp: number;
    armorValue: number;
    movementRange: number;
    attackStrategy: string;
    attack1Name: string;
    attack1Range: number; 
    attack1BaseDamage: number;
    attack2Name:string;
    attack2Range:number;
    attack2BaseDamage:number;
    ModifierDiceSides: number;
}

