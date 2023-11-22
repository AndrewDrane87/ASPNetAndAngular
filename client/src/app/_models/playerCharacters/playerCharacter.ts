import { Item } from "../item";

export interface PlayerCharacter{
    id: number;
    name: string;
    photoUrl: string;
    level: number;
    currentHitpoints: number;
    maxHitpoints : number;
    gold: number;
    leftHand: Item | undefined;
    rightHand: Item | undefined;
    helmet: Item | undefined;
    body: Item | undefined;
    feet: Item |undefined;
    backPack: (Item | undefined)[] | undefined;
}


