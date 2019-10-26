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

    static author = {
        books: {
            errorLoadingList: 'ERROR_LOADING_BOOKS_LIST',
            added: 'BOOK_ADDED',
            edited: 'BOOK_EDITED',
            deleted: 'BOOK_DELETED',
            deleteQuestion: 'BOOK_DELET_QUESTION'
        },

        prices: {
            added: 'BOOK_PRICE_ADDED',
            edited: 'BOOK_PRICE_EDITED',
            deleted: 'BOOK_PRICE_DELETED',
            deleteQuestion: 'BOOK_PRICE_DELETE_QUESTION'
        }
    }
}

export { SiteMessages };