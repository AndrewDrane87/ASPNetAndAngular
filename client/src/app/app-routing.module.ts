import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MessagesComponent } from './messages/messages/messages.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { memberDetailedResolver } from './_resolvers/member-detailed.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { adminGuard } from './_guard/admin.guard';
import { MyCharactersComponent } from './player/my-characters/my-characters.component';
import { LocationManagementComponent } from './admin/adventure/location-management/location-management.component';
import { locationManagementResolver } from './_resolvers/location-management.resolver';
import { AdventureManagementComponent } from './admin/adventure/adventure-management/adventure-management.component';
import { adventureManagementResolver, playerAdventureResolver } from './_resolvers/adventure-management.resolver';
import { NpcManagementComponent } from './admin/adventure/npc-management/npc-management.component';
import { npcManagementResolver } from './_resolvers/npc-management.resolver';
import { dialogueManagementResolver } from './_resolvers/dialogue-management.resolver';
import { DialogueManagementComponent } from './admin/adventure/dialogue-management/dialogue-management.component';
import { RegisterComponent } from './register/register.component';
import { AdventureSelectionComponent } from './play/views/adventure-selection/adventure-selection.component';
import { RunAdventureComponent } from './play/views/run-adventure/run-adventure.component';
import { DamageCalculatorComponent } from './admin/damage-calculator/damage-calculator.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'errors', component: TestErrorComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {path: 'playercharacters', component: MyCharactersComponent},
      {path: 'members', component: MemberListComponent},
      { path: 'members/:username', component: MemberDetailComponent, resolve:{member: memberDetailedResolver} },
      { path: 'member/edit', component: MemberEditComponent, canDeactivate:[preventUnsavedChangesGuard] },
      { path: 'messages', component: MessagesComponent },
      { path: 'admin', component: AdminPanelComponent, canActivate: [adminGuard] },
      { path: 'admin/location/:locationId', component: LocationManagementComponent, resolve:{location: locationManagementResolver} },
      { path: 'admin/npc/:npcId', component: NpcManagementComponent, resolve:{npc: npcManagementResolver} },
      { path: 'admin/dialogue/:Id', component: DialogueManagementComponent, resolve:{dialogue: dialogueManagementResolver} },
      { path: 'admin/adventure/:adventureId', component: AdventureManagementComponent, resolve:{adventure: adventureManagementResolver} },
      { path: 'play', component: AdventureSelectionComponent},
      { path: 'register', component: RegisterComponent},
      { path: 'damagecalculator', component: DamageCalculatorComponent},
      { path: 'admin/adventure/:adventureId', component: RunAdventureComponent, resolve:{adventure: adventureManagementResolver} },
      { path: 'player/adventure/:adventureId', component: RunAdventureComponent, resolve:{adventure: playerAdventureResolver} },
    ],
  },

  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
