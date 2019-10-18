import { Component, OnInit, ViewChild, ViewContainerRef, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { ListsService, UserService, PublicService } from '@br/core/services/';
import { PageRenderingService } from '@br/public/services';
import { PublicPageInfo } from '@br/core/models';
import { Router, RouterEvent, NavigationEnd } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  publicPageInfo: PublicPageInfo;
  test = 'AAAAA';


  @ViewChild('publicContent', { read: ViewContainerRef, static: false })
  publicContent: ViewContainerRef;

  constructor(
    private listsSvc: ListsService,
    public userSvc: UserService,
    public publicSvc: PublicService,
    private pageSvc: PageRenderingService,
    private router: Router,
    private changeDetector : ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
    this.listsSvc.init();

    this.router.events.subscribe((event: RouterEvent) => {

      if( event instanceof NavigationEnd){
        if( event.url == '/' 
              && this.publicPageInfo 
              && this.userSvc.isUiVisible) {
          this.userSvc.toggleUi(false); 

          // show client content again
          this.changeDetector.detectChanges();   

          this.pageSvc.compileTemplate(this.publicPageInfo.content, this.publicContent);
        }
      }
    });

    this.userSvc.init()
      .subscribe(val => {
        this.publicSvc.getPageInfo()
          .subscribe(val => {
            if (val) {
              this.publicPageInfo = val;

              if(!this.userSvc.authorized) {
                this.userSvc.toggleUi(false);
                // show client content again
                this.changeDetector.detectChanges(); 
                this.pageSvc.compileTemplate(val.content, this.publicContent);
              }
            }
          }, err => {
            debugger;
          })
      });



  }
}
