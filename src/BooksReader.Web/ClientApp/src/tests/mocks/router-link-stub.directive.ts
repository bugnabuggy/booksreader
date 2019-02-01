import { Directive, Input } from '@angular/core';

@Directive({
    selector: '[routerLink], [routerLinkActive], routerLinkActive, routerLink',
    // tslint:disable-next-line:use-host-property-decorator
    host: { '(click)': 'onClick()' }
  })
  export class RouterLinkStubDirective {
    // tslint:disable-next-line:no-input-rename
    @Input('routerLink') linkParams: any;
    // tslint:disable-next-line:no-input-rename
    @Input('routerLinkActive') activeClass: any;
    navigatedTo: any = null;  
    onClick() {
      this.navigatedTo = this.linkParams;
    }
  }
