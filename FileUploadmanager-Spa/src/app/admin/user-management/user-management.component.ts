import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RolesModelComponent } from '../roles-model/roles-model.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  bsModalRef: BsModalRef;
  constructor(
    private adminService: AdminService,
    private alertifyService: AlertifyService,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.adminService.getUserWithRoles().subscribe(
      (res: User[]) => {
        this.users = res;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
  editRoles(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user),
      title: 'Edit Role Management',
    };
    this.bsModalRef = this.modalService.show(RolesModelComponent, {
      initialState,
    });
    this.bsModalRef.content.updateSelectedroles.subscribe((values) => {
      const rolesToUpdate = {
        rolesName: [...values.filter((d) => d.checked).map((el) => el.name)],
      };
      this.adminService.updateRoles(user, rolesToUpdate).subscribe(
        () => {
          user.roles = [...rolesToUpdate.rolesName];
        },
        (error) => this.alertifyService.error(error)
      );
    });
  }

  getRolesArray(user: User) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any = [
      {
        name: 'Admin',
        value: 'Admin',
      },
      {
        name: 'Moderator',
        value: 'Moderator',
      },
      {
        name: 'Member',
        value: 'Member',
      },
      {
        name: 'Vip',
        value: 'Vip',
      },
    ];

    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }
}
