import { environment } from '@br/env/environment';



class Endpoints {
    static baseApiUrl = environment.baseApiUrl;

    static areas =  {
        user: 'user',
        reader: 'reader',
        author: 'author',
        admin: 'admin'
    };

    static forntend = {
        main: 'main',
        authorization: 'authorize',
        error: 'error',
        user: {
            profile: 'profile', 
            profileUrl: '/' + Endpoints.areas.user + '/profile',
            forceLogout: 'logout',
            becomeAnAuthor: 'become-an-author',
            becomeAnAuthorUrl: '/' + Endpoints.areas.user + '/become-an-author'
        },
        reader: {
            profile: 'profile',
            profileUrl: '/' +  Endpoints.areas.reader + '/profile',
            dashboard: 'dashboard',
            dashboardUrl: '/' +  Endpoints.areas.reader + '/dashboard'
        },
        admin: {
            dashboard: 'dashboard',
            dashboardUrl: '/' + Endpoints.areas.admin + '/dashboard',
            allUsers: 'users',
            allUsersUrl: '/' + Endpoints.areas.admin + '/users'
        },
        author: {
            profile: 'profile',
            profileUrl: '/' + Endpoints.areas.author + '/profile',
            dashboard: 'dashboard',
            dashboardUrl: '/' + Endpoints.areas.author + '/dashboard',
            book: 'book/:id/:tab',
            bookUrl: '/' + Endpoints.areas.author + '/book/:id/:tab',
        }
        
    };
    
    static api = {
        authorization: {
            login: Endpoints.baseApiUrl + 'connect/token',
            logout: Endpoints.baseApiUrl + 'connect/revocation',
            registration: Endpoints.baseApiUrl + 'api/identity/registration',
            antiforgery: Endpoints.baseApiUrl + 'api/identity/antiforgery',
            changePassword: Endpoints.baseApiUrl + 'api/identity/change-password',
        },
        user: {
            info: Endpoints.baseApiUrl + 'api/identity/me',
            profile: Endpoints.baseApiUrl + 'api/user/profile',
            loginHistory: Endpoints.baseApiUrl + 'api/identity/login-history',
            becomeAnAuthor: Endpoints.baseApiUrl + 'api/user/author'
        },
        reader: {
            books: Endpoints.baseApiUrl + 'api/books',
        },
        users: {
            all: Endpoints.baseApiUrl + 'api/admin/users',
            userRole: Endpoints.baseApiUrl + 'api/admin/users/{username}/{role}',
            delete: Endpoints.baseApiUrl + 'api/admin/users/{username}',
        },
        author: {
            profile: Endpoints.baseApiUrl + 'api/author/profile',
            book: Endpoints.baseApiUrl + 'api/author/book/{id}',
            bookFullEditInfo: Endpoints.baseApiUrl + 'api/author/book/{id}/edit',
            chapter: Endpoints.baseApiUrl + 'api/author/book/{bookId}/chapter/{id}',
        },
        admin: {
        },
        public: {
            pageInfo: Endpoints.baseApiUrl + 'api/public',
            lists:  Endpoints.baseApiUrl + 'api/public/lists',
        }

    };
}

export { Endpoints };
