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
        }
    };
}

export { Endpoints };
