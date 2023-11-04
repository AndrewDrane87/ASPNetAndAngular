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