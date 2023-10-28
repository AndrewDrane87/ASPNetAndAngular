import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dialogue, NPC } from 'src/app/_models/npc';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NpcService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  createNPC(npc:NPC, locationId: number){
    var url = this.baseUrl + 'npc/create';
    return this.http.post<NPC>(url,{'locationId' : locationId,'name': npc.name, 'caption': npc.caption})
  }

  deleteNpc(npc: NPC){
    var url = this.baseUrl + `npc/delete?id=${npc.id}`;
    return this.http.delete(url);
  }
  getNpcDetail(npcId:number){
    var url = this.baseUrl + `npc/get?id=${npcId}`;
    return this.http.get<NPC>(url)
  }

  getDialogueDetail(id: number){
    var url = this.baseUrl + `dialogue/get-dialogue?dialogueId=${id}`
    return this.http.get<Dialogue>(url);
  }

  createResponse(textValue: string, dialogueId: number){
    var url = this.baseUrl + `dialogue/create-response?dialogueId=${dialogueId}`
    console.log('URL: ' + url)
    return this.http.post<Dialogue>(url,textValue);
  }

  getDialogueByResponseId(id: number){
    var url = this.baseUrl + `dialogue/get-dialogue-from-response?responseId=${id}`
    return this.http.get<Dialogue>(url);
  }

  deleteResponse(id: number){
    var url = this.baseUrl + `dialogue/delete-response?responseId=${id}`
    console.log('URL: ' + url)
    return this.http.delete<Dialogue>(url);
  }

  createChildDialogue(textValue: string, responseId: number){
    var url = this.baseUrl + `dialogue/create-child-dialogue?responseId=${responseId}`
    return this.http.post<Dialogue>(url, textValue)
  }

  getPreviousDialogue(dialogueId: number){
    var url = this.baseUrl + `dialogue/get-previous-dialogue?dialogueId=${dialogueId}`
    return this.http.get<Dialogue>(url);
  }
  
}
