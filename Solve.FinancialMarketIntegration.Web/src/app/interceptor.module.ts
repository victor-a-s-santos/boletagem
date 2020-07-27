import { Injectable, NgModule } from '@angular/core';
import { Observable } from 'rxjs';
import {
 HttpEvent,
 HttpInterceptor,
 HttpHandler,
 HttpRequest,
 HttpResponse
} from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { tap } from 'rxjs/operators';

import { LoaderService } from './services/loader.service';

@Injectable()

export class HttpsRequestInterceptor implements HttpInterceptor {
constructor (
  private loaderService: LoaderService
) { }

  intercept(
  req: HttpRequest<any>,
  next: HttpHandler,
  ): Observable<HttpEvent<any>> {
    this.loaderService.addLoader();

    return next
      .handle(req)
      .pipe(
        tap(() => {
          this.loaderService.removeLoader();
        })
      );
  }
}

@NgModule({
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpsRequestInterceptor,
      multi: true,
    },
  ],
})


export class Interceptor {}
