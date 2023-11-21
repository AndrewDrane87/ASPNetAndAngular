import { Enemy } from "./AdventureSave";
import { ActionTrigger } from "./actionTrigger";
import { Item } from "./item";
import { NPC } from "./npc";

export interface AdminAdventure{
    id: number;
    name: string;
    description : string;
    startingLocation : AdminAdventureLocation;
    locations: AdminAdventureLocation[];
    variables: AdminAdventureVariable;

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
    enemies: Enemy;
    visibilityRequirements: string;
    roomNumber: number;
}

export interface NewLocation{
    name: string;
    shortDescription: string;
    description : string;
    roomNumber: number;
}

export interface AdminContainer{
    id: number;
    name: string;
    description: string;
    items: Item[];
}

export interface AdminAdventureVariable{
    id: number;
    name: string;
    initialValue: string;
}

export interface AdminInteraction{
    id: number;
    name: string;
    information: string;
    locationId: number;
    triggers: ActionTrigger[];
}