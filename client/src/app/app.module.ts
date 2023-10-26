import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists/lists.component';
import { MessagesComponent } from './messages/messages/messages.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStrategy } from './_services/customRouteReuseStrategy';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { MyCharactersComponent } from './player/my-characters/my-characters.component';
import { CreateCharacterComponent } from './player/create-character/create-character/create-character.component';
import { ItemCardComponent } from './player/item-card/item-card.component';
import { ItemManagerComponent } from './admin/items/item-manager/item-manager.component';
import { ItemPhotoUploaderComponent } from './admin/items/item-photo-uploader/item-photo-uploader.component';
import { CreateItemFormComponent } from './admin/items/create-item-form/create-item-form.component';
import { ItemTypeSelectorComponent } from './admin/items/item-type-selector/item-type-selector.component';
import { ImageSelectorComponent } from './image-selector/image-selector.component';
import { CharacterDisplayComponent } from './player/character-display/character-display.component';
import { ItemSelectorComponent } from './player/item-selector/item-selector.component';
import { CreateAdventureFormComponent } from './admin/adventure/create-adventure-form/create-adventure-form.component';
import { AdventureAdminComponent } from './admin/adventure/adventure-admin/adventure-admin.component';
import { CreateLocationFormComponent } from './admin/adventure/create-location-form/create-location-form.component';
import { CreateNameDescriptionComponent } from './admin/modals/create-name-description/create-name-description.component';
import { OpenModalComponent } from './examples/modals/open-modal/open-modal.component';
import { ExampleModalComponent } from './examples/modals/example-modal/example-modal.component';
import { CreateLocationLinkComponent } from './admin/modals/create-location-link/create-location-link.component';
import { LocationManagementComponent } from './admin/adventure/location-management/location-management.component';
import { AdventureManagementComponent } from './admin/adventure/adventure-management/adventure-management.component';
import { CreateNpcComponent } from './admin/modals/create-npc/create-npc.component';
import { CreateContainerComponent } from './admin/modals/create-container/create-container.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DatePickerComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    PhotoManagementComponent,
    RolesModalComponent,
    ConfirmDialogComponent,
    MyCharactersComponent,
    CreateCharacterComponent,
    ItemCardComponent,
    ItemManagerComponent,
    ItemPhotoUploaderComponent,
    CreateItemFormComponent,
    ItemTypeSelectorComponent,
    ImageSelectorComponent,
    CharacterDisplayComponent,
    ItemSelectorComponent,
    CreateAdventureFormComponent,
    AdventureAdminComponent,
    CreateLocationFormComponent,
    CreateNameDescriptionComponent,
    OpenModalComponent,
    ExampleModalComponent,
    CreateLocationLinkComponent,
    LocationManagementComponent,
    AdventureManagementComponent,
    CreateNpcComponent,
    CreateContainerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: RouteReuseStrategy, useClass: CustomRouteReuseStrategy },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
