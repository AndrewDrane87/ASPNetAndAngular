export interface NPC{
    id: number;
    name:string;
    caption: string,
    dialogue : Dialogue
}

export interface Dialogue{
id: number;
text: string;
responses: DialogueResponse[]
}

export interface DialogueResponse{
    id: number;
    text: string;
    childDialogue: Dialogue;
}