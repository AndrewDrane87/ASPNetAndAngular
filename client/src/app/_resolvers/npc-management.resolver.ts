import { ResolveFn } from '@angular/router';
import { LocationService } from '../_services/adventures/locationService';
import { inject } from '@angular/core';
import { NPC } from '../_models/npc';
import { NpcService } from '../_services/adventures/npc.service';

export const npcManagementResolver: ResolveFn<NPC> = (route, state) => {
  const npcService = inject(NpcService);
  return npcService.getNpcDetail(Number(route.paramMap.get('npcId'))!);
};
