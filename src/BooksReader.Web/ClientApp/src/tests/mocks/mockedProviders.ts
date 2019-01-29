import { UserService,
         NotificationService,
         SecurityService,
         AdminUsersService,
         BooksService
         } from '../../app/services';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Observable } from 'rxjs';
import { AppUser } from '../../app/models';

const MOCKED_PROVIDERS = [
    {provide: SecurityService, useValue: { projectData: [], token : '', user: {} as AppUser,   isAuthenticated: () => !!this.token } },
    {provide: UserService, useValue: {} },
    {provide: Title, useValue: {} },
    {provide: NotificationService, useValue: {} },
    {provide: MatSnackBar, useValue: {} },
    {provide: MatDialogRef, useValue: {} },
    {provide: MAT_DIALOG_DATA, useValue: {} },
    {provide: Router, useValue: {} },
    {provide: AdminUsersService, useValue: { getUsers() { return new Observable; } } },
    {provide: BooksService, useValue: {} }

];

export { MOCKED_PROVIDERS };
