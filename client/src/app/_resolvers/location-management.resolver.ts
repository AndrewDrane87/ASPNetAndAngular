import { ResolveFn } from '@angular/router';
import { LocationService } from '../_services/adventures/locationService';
import { AdventureLocation } from '../_models/Adventure';
import { inject } from '@angular/core';

export const locationManagementResolver: ResolveFn<AdventureLocation> = (route, state) => {
  const locationService = inject(LocationService);
  return locationService.getLocationDetail(Number(route.paramMap.get('locationId'))!);
};
