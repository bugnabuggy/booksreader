import { Component, OnInit, Input } from '@angular/core';
import { BookMarketDto, EmptyBookMarketDto } from '@br/core/models/api/dto/public';
import { BookMarketService } from '@br/core/services';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from '@br/core/services';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  book: BookMarketDto = EmptyBookMarketDto;
  
  constructor(
    private bookMarketSvc:  BookMarketService,
    private route: ActivatedRoute,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(x=> {
      debugger;
      if(x.id){
        this.bookMarketSvc.get(x.id).subscribe(x => {
          this.book = x;
        }, err=>{
          this.notifications.showError(err);
        });
      }
    })
  }

}
