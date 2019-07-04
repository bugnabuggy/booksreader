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
            profileUrl: Endpoints.areas.user + '/profile',
            forceLogout: 'logout',
        },
        reader: {
            dashboardUrl: Endpoints.areas.reader + '/dashboard'
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
        },
        reader: {
            books: Endpoints.baseApiUrl + 'api/books',
        },
        users: {
            all: Endpoints.baseApiUrl + 'api/admin/users',
            userRole: Endpoints.baseApiUrl + 'api/admin/users/{username}/{role}',
        },
        author: {
            book: Endpoints.baseApiUrl + 'api/author/book/{id}',
            chapter: Endpoints.baseApiUrl + 'api/author/book/{bookId}/chapter/{id}',
        },
        admin: {
        }
    };
}

export { Endpoints };
