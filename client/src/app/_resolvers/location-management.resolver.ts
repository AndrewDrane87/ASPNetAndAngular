import { ResolveFn } from '@angular/router';
import { LocationService } from '../_services/adventures/locationService';
import { AdminAdventureLocation } from '../_models/Adventure';
import { inject } from '@angular/core';

export const locationManagementResolver: ResolveFn<AdminAdventureLocation> = (route, state) => {
  const locationService = inject(LocationService);
  return locationService.getLocationDetail(Number(route.paramMap.get('locationId'))!);
};
