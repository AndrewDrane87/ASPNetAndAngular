export interface PlayerCharacter{
    id: number;
    name: string;
    photoUrl: string;
    leftHand: HandItem | undefined;
    rightHand: HandItem | undefined;
    helmet: Helmet | undefined;
    body: Armor | undefined;
    feet: Boots |undefined;
}


export interface HandItem{
    id: number;
    name: string;
    photoUrl: string;
    armorValue: number;
    attackValue: number;
    damageType: number;
}

export interface Helmet{
    id: number;
    name: string;
    photoUrl: string;
    armorValue: number;
}

export interface Armor{
    id: number;
    name: string;
    photoUrl: string;
    armorValue: number;
}

export interface Boots{
    id: number;
    name: string;
    photoUrl: string;
    armorValue: number;
}