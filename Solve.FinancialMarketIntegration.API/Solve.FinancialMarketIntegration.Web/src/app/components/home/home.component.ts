import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router) { }

  isRoot: boolean;

  ngOnInit(): void {
    this.listenRouterEvent();
  }

  /**
   * @description Listen to changes in router event to set if current URL is root
   */
  private listenRouterEvent(): void {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(routeEvent => {
        this.setIsRoot(routeEvent['url']);
      });
  }

  private setIsRoot(url: string): void {
    this.isRoot = url === '/' ? true : false;
  }

  toggleSidebar(): void { }
}
