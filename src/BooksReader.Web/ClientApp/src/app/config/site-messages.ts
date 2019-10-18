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
        verificationToggled: 'DOMAIN_VERIFICATION_TOGGLED'
    }

    static publicPages = {
        added: 'PUBLIC_PAGE_ADDED',
        saved: 'PUBLIC_PAGE_SAVED',
        deleted: 'PUBLIC_PAGE_DELETED'
    }
}

export { SiteMessages };