export interface ActionTrigger{
    id: number,
    actionTriggerId: number;
    complete : boolean;
    eventType: string,
    actionType: string,
    actionData: string,
    resultData: string,
}