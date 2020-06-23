import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Pipe } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { FileUploadModule } from 'ng2-file-upload';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor.service';
import { RoutesModule } from './routes';
import { ControlMessageComponent } from './_helper/control-message/control-message.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasroleDirective } from './_directive/hasrole.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModelComponent } from './admin/roles-model/roles-model.component';
import { FileUploaderComponent } from './fileuploader/file-uploader/file-uploader.component';
import { FileImportComponent } from './fileuploader/file-import/file-import/file-import.component';
import { ImportedfileListResolver } from './_resolver/importedfile-list-resolver';
import { LoaderComponent } from './loader/loader/loader.component';
import { LoaderInterceptorService } from './_services/loader-interceptor.service';

export function tokenGenerator() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    RegisterComponent,
    AdminPanelComponent,
    HasroleDirective,
    UserManagementComponent,
    RolesModelComponent,
    ControlMessageComponent,
    FileUploaderComponent,
    FileImportComponent,
    LoaderComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    ButtonsModule.forRoot(),
    RoutesModule,
    ModalModule.forRoot(),
    FileUploadModule,
    NgxSpinnerModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGenerator,
        whitelistedDomains: ['localhost:61541'],
        blacklistedRoutes: ['localhost:5000/api/auth'],
      },
    }),
  ],
  providers: [
    ErrorInterceptorProvider,
    ImportedfileListResolver,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
  entryComponents: [RolesModelComponent],
})
export class AppModule {}
