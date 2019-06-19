import { 
         AdminUsersService,
         ReaderBooksService
         } from '../../app/services';

import {
    UserService,
         NotificationService,
         SecurityService,
} from '@br/core/services'

import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Observable } from 'rxjs';

import { AppUser } from '@br/core/models';
import { MockStorageService } from './services';

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
    {provide: ReaderBooksService, useValue: { getBooks() { return new Observable; }}},
    {provide: Storage, useValue: new MockStorageService()}

];

export { MOCKED_PROVIDERS };
