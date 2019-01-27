import { environment } from '../../environments/environment';

class Endpoints {
    static baseApiUrl = environment.baseApiUrl;

    static forntend = {
    };

    static api = {
        authorization: {
            login: Endpoints.baseApiUrl + 'connect/token',
            logout: Endpoints.baseApiUrl + 'connect/revocation',
            registration: Endpoints.baseApiUrl + 'api/identity/registration',
            antiforgery: Endpoints.baseApiUrl + 'api/identity/antiforgery'
        },
        user: {
            info: Endpoints.baseApiUrl + 'api/identity/me'
        },
        users: {
            all: Endpoints.baseApiUrl + 'api/admin/users',
            userRole: Endpoints.baseApiUrl + 'api/admin/users/{username}/{role}',
        }
    };
}

export { Endpoints };
