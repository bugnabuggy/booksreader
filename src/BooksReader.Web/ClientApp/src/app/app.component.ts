import { Component, OnInit } from '@angular/core';
import { SecurityService, UserService } from '@br/core/services';
import { PublicService } from '@br/core/services/public.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  showRoutedContent = false;

  constructor(
    public userSvc: UserService,
    public publicSvc: PublicService
  ) {}

  ngOnInit() {
    this.userSvc.init();    
    this.publicSvc.getPageInfo().subscribe(val=>{
      if(!val){
        this.showRoutedContent = true;
      }
    }, err=>{
      this.showRoutedContent = true;
    });
  }
}
