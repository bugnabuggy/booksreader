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
        saved: 'DOMAIN_SAVED',
        deleted: 'DOMAIN_DELETED',
    }

    static publicPages = {
        added: 'PUBLIC_PAGE_ADDED',
        deleted: 'PUBLIC_PAGE_DELETED'
    }
}

export { SiteMessages };