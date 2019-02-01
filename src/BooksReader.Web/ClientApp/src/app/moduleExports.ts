import {
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
} from '@angular/material';

import { CdkTableModule } from '@angular/cdk/table';

const MATERIAL_DESIGN = [
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    CdkTableModule
];

import {
    DashboardComponent,
    RegistrationComponent,
    LoginComponent,
    ForceLogoutComponent,
    AdminDashboardComponent,
    AuthorDashboardComponent,
    BookMarketComponent,
    BookEditComponent
} from './pages';

import {
    BookEditDialogComponent, BookComponent, EditableBookComponent
} from './components';


import {
    UserService,
    NotificationService,
    SecurityService,
    AdminUsersService,
    AuthorBooksService,
    ReaderBooksService,
    BookEditService
} from './services';
import { UserHubService } from './hubs';



const MODULE_PIPES = [

];

const MODULE_COMPONENTS = [
    DashboardComponent,
    RegistrationComponent,
    LoginComponent,
    ForceLogoutComponent,
    AdminDashboardComponent,
    AuthorDashboardComponent,
    BookEditDialogComponent,
    BookMarketComponent,
    BookComponent,
    EditableBookComponent,
    BookEditComponent
];

const MODULE_SERVICES = [
    UserService,
    NotificationService,
    SecurityService,
    UserHubService,
    AdminUsersService,
    AuthorBooksService,
    ReaderBooksService,
    BookEditService
];

const MODULE_ENTRY_COMPONENTS = [
    BookEditDialogComponent
];

export { MODULE_COMPONENTS, MODULE_PIPES, MODULE_SERVICES, MATERIAL_DESIGN, MODULE_ENTRY_COMPONENTS};
