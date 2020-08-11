import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminPanelComponent } from '../admin/admin-panel/admin-panel.component';
import { UserManagementComponent } from '../admin/user-management/user-management.component';
import { RouterModule } from '@angular/router';
import { AdminRouteModule } from './admin-route-module';
import { TabsModule } from 'ngx-bootstrap/tabs';

@NgModule({
  imports: [CommonModule, AdminRouteModule, TabsModule.forRoot()],
  declarations: [AdminPanelComponent, UserManagementComponent],
})
export class AdminPanelModule {}
