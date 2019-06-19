import {
    DashboardComponent,
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
    AdminUsersService,
    AuthorBooksService,
    ReaderBooksService,
    BookEditService
} from './services';


const MODULE_COMPONENTS = [
    DashboardComponent,
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
    AdminUsersService,
    AuthorBooksService,
    ReaderBooksService,
    BookEditService
];

const MODULE_ENTRY_COMPONENTS = [
    BookEditDialogComponent
];

export { MODULE_COMPONENTS,  MODULE_SERVICES, MODULE_ENTRY_COMPONENTS};
