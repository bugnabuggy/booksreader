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
        authorization: 'authorize',
        error: 'error',
        user: {
            profile: Endpoints.areas.user + '/profile'
        },
        reader: {
            dashboard: Endpoints.areas.reader + '/dashboard'
        }
        
    };
    
    static api = {
        authorization: {
            login: Endpoints.baseApiUrl + 'connect/token',
            logout: Endpoints.baseApiUrl + 'connect/revocation',
            registration: Endpoints.baseApiUrl + 'api/identity/registration',
            antiforgery: Endpoints.baseApiUrl + 'api/identity/antiforgery'
        },
        user: {
            info: Endpoints.baseApiUrl + 'api/identity/me',
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
