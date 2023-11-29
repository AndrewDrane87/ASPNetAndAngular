export interface Item{
    id: number;
    name: string;
    requiredLevel: number;
    photoUrl: string ;
    attackValue: number;
    armorValue: number;
    damageModifiers: string;
    statModifiers: string;
    resistanceModifiers: string;
    itemType: string;
    damageType: string,
    diceSides: number,
    storageIndex: number;
    value: number;
    maxStackSize: number;
    currentStackSize: number;
    use: string;
    cost: number;
}