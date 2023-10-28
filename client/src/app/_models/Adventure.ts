import { Item } from "./item";
import { NPC } from "./npc";

export interface Adventure{
    id: number;
    name: string;
    description : string;
    startingLocation : AdventureLocation;
    locations: AdventureLocation[];

}

export interface AdventureLocation{
    id: number;
    name: string;
    description : string;
    npCs: NPC[],
    containers: Container[]
    connectedLocations: AdventureLocation[]
    interactions: Interaction[]
}

export interface Container{
    id: number;
    name: string;
    description: string;
    items: Item[];
}

export interface Interaction{
    id: number,
    name: string,
    information: string
    locationId: number
}