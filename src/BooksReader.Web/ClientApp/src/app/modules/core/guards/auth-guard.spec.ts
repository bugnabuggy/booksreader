import { TestBed } from "@angular/core/testing";
import { AuthGuard } from './auth-guard';
import { RouterTestingModule } from '@angular/router/testing';
import { SecurityService } from '../services';
import { of, BehaviorSubject, Observable, Subscriber } from 'rxjs';

let spy: Subscriber<boolean>;
let securityMock = {
    isLoggedIn$: new Observable((subscriber)=>{
        spy = subscriber;
    })

} as SecurityService;


describe('Auth Guard Tests', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [
            RouterTestingModule,
        ],
        providers:[
            {
                provide: SecurityService,
                useValue: securityMock
            }
        ]
    }));
  
    it('should be created', () => {
        
      const guard: AuthGuard = TestBed.get(AuthGuard);
      expect(guard).toBeTruthy();
    });

    it('should return observable for guard', (done) => {
        const guard: AuthGuard = TestBed.get(AuthGuard);
        var obs$ = guard.canActivate({} as any, {} as any);
        expect(obs$).toBeTruthy();
        expect(typeof(obs$.subscribe) === 'function').toBeTruthy();

        // happy path → user logged in
        var sub = obs$.subscribe(val=>{
            expect(val).toBeTruthy();
        })

        spy.next(true);

        sub.unsubscribe();

        // bad path → user not logged in, or error occured
        sub = obs$.subscribe(val=>{
            expect(val).toBeFalsy();
            done();
        })

        spy.next(false);
    })
  });
  