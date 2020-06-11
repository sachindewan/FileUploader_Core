import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { Filedescription } from 'src/app/_models/filedescription';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-file-import',
  templateUrl: './file-import.component.html',
  styleUrls: ['./file-import.component.css'],
})
export class FileImportComponent implements OnInit {
  uploader: FileUploader;
  hasBaseDropZoneOver = true;
  baseUrl = environment.baseUrl;
  files: Filedescription[] = [];
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.files = data['files'];
    });
    this.initializeUploader();
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
        'FileUploader/importfile',
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
}
