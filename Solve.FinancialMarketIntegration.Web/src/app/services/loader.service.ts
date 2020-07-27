import { Injectable } from '@angular/core';
import { NgxUiLoaderConfig, NgxUiLoaderService } from 'ngx-ui-loader';

@Injectable()
export class LoaderService {

  config: NgxUiLoaderConfig;
  public contador = 0;

  /**
   * Constructor
   * @param ngxUiLoaderService
   */
  constructor(private ngxUiLoaderService: NgxUiLoaderService) {
    this.config = this.ngxUiLoaderService.getDefaultConfig();
  }

  getLoader() {
    return this.ngxUiLoaderService.getLoader();
  }

  private startLoader(sliderId: string = 'master') {
    this.ngxUiLoaderService.startLoader(sliderId);
  }

  private stopLoader(sliderId: string = 'master') {
    this.ngxUiLoaderService.stopLoader(sliderId);
  }

  public addLoader() {
    if (this.contador === 0) {
      this.startLoader();
    }

    this.contador++;
  }

  public removeLoader() {
    if (this.contador > 0) {
      this.contador--;
    }

    if (this.contador === 0) {
      this.stopLoader();
    }
  }
}
