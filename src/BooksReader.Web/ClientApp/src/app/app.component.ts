import { Component, OnInit, ViewChild, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
import { SecurityService, UserService, ListsService } from '@br/core/services';
import { PublicService } from '@br/core/services/public.service';
import { PublicPageInfo } from '@br/core/models';
import { PageRenderingService } from '@br/public/services';
import { Router, ActivatedRoute, NavigationEnd, RouterEvent } from '@angular/router';
import { Endpoints } from './config';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  publicPageInfo : PublicPageInfo;
  
  @ViewChild('publicContent', {read : ViewContainerRef, static: false}) 
  publicContent: ViewContainerRef;

  constructor(
    public userSvc: UserService,
    public publicSvc: PublicService,
    public listsSvc: ListsService,
    public pageRenderingSvc:  PageRenderingService,
    public router: Router,
    private changeDetector : ChangeDetectorRef
  ) {}


// TODO: refactor this after reconsidering possible application states
  ngOnInit() {
    this.listsSvc.init();
    
    this.router.events.subscribe((event: RouterEvent) => {

      if(event instanceof NavigationEnd){
        if(event.url == '/' && this.publicPageInfo && this.userSvc.isUiVisible) {
          this.userSvc.hideUi(); 

          // show client content again
          this.changeDetector.detectChanges();   
          this.pageRenderingSvc.compileTemplate(this.publicPageInfo.content, this.publicContent);
        }
      }
    });

    this.userSvc.init()
    .subscribe(val=>{
      this.publicSvc.getPageInfo().subscribe(val => {
        // TODO: think how to show control UI components to be able to navigate to management pages
        this.publicPageInfo = val || null;
        
        let isloggedIn = this.userSvc.isLoggedIn;
        
        // help to bind view child
        this.changeDetector.detectChanges();

        const isUrlMatch = this.router.url === (val && (val.path || '/'));
        if(!val && !isloggedIn) {
          this.router.navigate([Endpoints.forntend.main]);
        } else {
          if(isUrlMatch && this.publicContent) {
            this.userSvc.hideUi();  
            this.pageRenderingSvc.compileTemplate(val.content, this.publicContent);
          }
          this.userSvc.showUi();
        }
      }, err => {
        console.error(err);
        this.userSvc.showUi();
      });
    });    
  }

  ngAfterViewInit(){
    console.log(this.publicContent);
  }
}
