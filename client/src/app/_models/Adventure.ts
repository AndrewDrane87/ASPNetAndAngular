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
}

export interface Container{
    id: number;
    name: string;
    description: string;
    items: Item[];
}