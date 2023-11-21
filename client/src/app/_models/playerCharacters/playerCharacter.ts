import { Item } from "../item";

export interface PlayerCharacter{
    id: number;
    name: string;
    photoUrl: string;
    leftHand: Item | undefined;
    rightHand: Item | undefined;
    helmet: Item | undefined;
    body: Item | undefined;
    feet: Item |undefined;
    backPack: (Item | undefined)[] | undefined;
}


