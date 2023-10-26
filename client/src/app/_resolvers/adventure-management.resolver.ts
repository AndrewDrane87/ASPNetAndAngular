import { ResolveFn } from '@angular/router';
import { Adventure } from '../_models/Adventure';
import { AdventureService } from '../_services/adventures/adventureService';
import { inject } from '@angular/core';

export const adventureManagementResolver: ResolveFn<Adventure> = (route, state) => {
  const adventureService = inject(AdventureService);
  return adventureService.getAdventureAdmin(Number(route.paramMap.get('adventureId'))!);
};
