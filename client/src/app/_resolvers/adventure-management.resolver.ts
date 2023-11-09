import { ResolveFn } from '@angular/router';
import { AdminAdventure } from '../_models/Adventure';
import { AdventureService } from '../_services/adventures/adventureService';
import { inject } from '@angular/core';
import { Adventure } from '../_models/AdventureSave';
import { AdventureAdminService } from '../_services/adventure-admin.service';

export const adventureManagementResolver: ResolveFn<AdminAdventure> = (route, state) => {
  const adventureService = inject(AdventureAdminService);
  return adventureService.getAdventureAdmin(Number(route.paramMap.get('adventureId'))!);
};

export const playerAdventureResolver: ResolveFn<Adventure> = (route, state) => {
  const adventureService = inject(AdventureService);
  return adventureService.getAdventurePlayer(Number(route.paramMap.get('adventureId'))!);
};
