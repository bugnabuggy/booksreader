import { Component, OnInit, ViewChild, ViewContainerRef, ChangeDetectorRef, AfterViewInit, Inject, PLATFORM_ID, Compiler } from '@angular/core';
import { ListsService, UserService, PublicService } from '@br/core/services/';
import { PageRenderingService } from '@br/public/services';
import { PublicPageInfo } from '@br/core/models';
import { Router, RouterEvent, NavigationEnd } from '@angular/router';
import { Location, isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  publicPageInfo: PublicPageInfo;
  isBrowser = true;
  error = '';

  @ViewChild('publicContent', { read: ViewContainerRef, static: false })
  publicContent: ViewContainerRef;

  constructor(
    private listsSvc: ListsService,
    public userSvc: UserService,
    public publicSvc: PublicService,
    private pageSvc: PageRenderingService,
    private router: Router,
    private changeDetector: ChangeDetectorRef,
    private compiler: Compiler,
    @Inject(PLATFORM_ID) private platformId: Object,
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
    
    // ↓ bad practice! replace with resolver?
    if (!this.isBrowser) {
      this.loadPublicInfo();
    }
  }

  ngOnInit(): void {
    this.listsSvc.init();

    this.router.events.subscribe((event: RouterEvent) => {
      if (event instanceof NavigationEnd) {
        if (event.url == '/'
          && this.publicPageInfo
          && this.userSvc.isUiVisible) {
            this.showClientPage();
        }
      }
    });

    this.userSvc.init()
      .subscribe(val => {
        this.loadPublicInfo();
      });
  }

  showClientPage() {
    this.userSvc.toggleUi(false);
    // show client content again
    this.changeDetector.detectChanges();
    this.pageSvc.compileTemplate(this.compiler, this.publicPageInfo.content, this.publicContent);
  }

  loadPublicInfo() {
    // do not load twice
    if (this.publicPageInfo) return;

    const observable = this.publicSvc.getPageInfo();

    observable
      .subscribe(val => {
          if (val) {
            this.publicPageInfo = val;

            if (!this.userSvc.authorized) {
                this.showClientPage();
            }
          }
        }, err => {
          this.error = err;
        });

      return observable;
  }
}
