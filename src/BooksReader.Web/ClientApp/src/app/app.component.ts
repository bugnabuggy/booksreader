import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { SecurityService, UserService } from '@br/core/services';
import { PublicService } from '@br/core/services/public.service';
import { PublicPageInfo } from '@br/core/models';
import { PageRenderingService } from '@br/public/services';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  showRoutedContent = false;
  publicPageInfo : PublicPageInfo;
  
  @ViewChild('publicContent', {read : ViewContainerRef, static: false}) 
  publicContent: ViewContainerRef;

  constructor(
    public userSvc: UserService,
    public publicSvc: PublicService,
    public pageRenderingSvc:  PageRenderingService
  ) {}

  ngOnInit() {
    this.userSvc.init()
    .subscribe(val=>{
      this.publicSvc.getPageInfo().subscribe(val => {
        debugger;
        // TODO: think how to show control UI components to be able to navigate to management pages
        if(!val){
          this.showRoutedContent = true;
        } else {
          this.publicPageInfo = val;
          this.pageRenderingSvc.compileTemplate(val.content, this.publicContent);
        }
      }, err => {
        console.error(err);
        this.showRoutedContent = true;
      });
    });    
  }

  ngAfterViewInit(){
    console.log(this.publicContent);
    
  }
}
