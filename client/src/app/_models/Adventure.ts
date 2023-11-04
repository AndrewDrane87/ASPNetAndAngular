import { ActionTrigger } from "./actionTrigger";
import { Item } from "./item";
import { NPC } from "./npc";

export interface AdminAdventure{
    id: number;
    name: string;
    description : string;
    startingLocation : AdminAdventureLocation;
    locations: AdminAdventureLocation[];

}

export interface AdminAdventureLocation{
    id: number;
    name: string;
    description : string;
    npCs: NPC[],
    containers: AdminContainer[]
    connectedLocations: AdminAdventureLocation[]
    interactions: AdminInteraction[]
    triggers: ActionTrigger[];
}

export interface AdminContainer{
    id: number;
    name: string;
    description: string;
    items: Item[];
}

export interface AdminInteraction{
    id: number;
    name: string;
    information: string;
    locationId: number;
    triggers: ActionTrigger[];
}