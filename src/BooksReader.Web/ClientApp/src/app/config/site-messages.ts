class SiteMessages {
    static user = {
            registration: {
                success: "REGISTRATION_SUCCESS"
            },
            profile: {
                updated: "PROFILE_UPDATED"
            },
            userDeleted: "USER_DELETED"
        }
    
    static errors = {
        anyError: "ERROR"
    }

    static system = {
        noForceLogoutCallback: 'NO_FORCE_LOGOUT_CALLBACK',
        errorWhileRequest: 'ERROR_WHILE_REQUEST'
    }
    
    static domains = {
        added: 'DOMAIN_ADDED',
        deleted: 'DOMAIN_DELETED',
    }
}

export { SiteMessages };