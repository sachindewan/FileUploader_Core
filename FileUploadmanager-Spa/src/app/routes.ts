import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { FileUploaderComponent } from './fileuploader/file-uploader/file-uploader.component';
import { FileImportComponent } from './fileuploader/file-import/file-import/file-import.component';
import { ImportedfileListResolver } from './_resolver/importedfile-list-resolver';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'fileupload',
        component: FileUploaderComponent,
      },
      {
        path: 'fileimport',
        component: FileImportComponent,
        resolve: { files: ImportedfileListResolver },
      },
      {
        path: 'admin',
        component: AdminPanelComponent,
        data: { roles: ['Admin', 'Moderator'] },
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full',
  },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      enableTracing: false, // <-- debugging purposes only
    }),
  ],
  exports: [RouterModule],
  providers: [],
})
export class RoutesModule {}
