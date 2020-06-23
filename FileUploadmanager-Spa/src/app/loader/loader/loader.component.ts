import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoaderService } from '../../_services/loader.service';
import { LoaderState } from '../../_models/loader-state';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css'],
})
export class LoaderComponent implements OnInit, OnDestroy {
  show = false;
  private subscription: Subscription;
  constructor(
    private loaderService: LoaderService,
    private spinner: NgxSpinnerService
  ) {}
  ngOnInit() {
    this.subscription = this.loaderService.loaderState.subscribe(
      (state: LoaderState) => {
        if (state.show) {
          this.spinner.show();
        } else {
          this.spinner.hide();
        }
      }
    );
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
