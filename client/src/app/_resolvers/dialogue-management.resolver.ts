import { ResolveFn } from '@angular/router';
import { LocationService } from '../_services/adventures/locationService';
import { inject } from '@angular/core';
import { Dialogue } from '../_models/npc';
import { NpcService } from '../_services/adventures/npc.service';

export const dialogueManagementResolver: ResolveFn<Dialogue> = (route, state) => {
  const npcService = inject(NpcService);
  return npcService.getDialogueDetail(Number(route.paramMap.get('Id'))!);
};
