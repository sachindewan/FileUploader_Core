import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { environment } from 'src/environments/environment';
import { Filedescription } from 'src/app/_models/filedescription';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.css'],
})
export class FileUploaderComponent implements OnInit {
  uploader: FileUploader;
  hasBaseDropZoneOver = true;
  baseUrl = environment.baseUrl;
  files: Filedescription[];
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.initializeUploader();
    this.getAllFiles();
  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
  initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        'user/' +
        this.authService.decodedToken.nameid +
        '/' +
        'FileUploader/uploadfile',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 200 * 1024 * 1024,
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, header) => {
      debugger;
      if (response) {
        this.files.push(JSON.parse(response));
        this.alertify.success('file has been uploaded sucessfully');
      }
    };
    this.uploader.onErrorItem = (
      item: any,
      response: string,
      status: number,
      headers
    ) => {
      this.alertify.error(response);
    };
  }

  getAllFiles() {
    this.userService
      .getAllFilesDesc(this.authService.decodedToken.nameid)
      .subscribe(
        (res) => {
          this.files = res;
        },
        (error) => this.alertify.error(error)
      );
  }
  deleteFile(fileId: number) {
    this.alertify.confirm('do you want to delete?', () => {
      this.userService
        .deleteUserFile(this.authService.decodedToken.nameid, fileId)
        .subscribe(
          (res) => {
            this.files.splice(
              this.files.findIndex((x) => x.id === fileId),
              1
            );
            this.alertify.success('file has been deleted');
          },
          (error) => this.alertify.error(error)
        );
    });
  }
}
