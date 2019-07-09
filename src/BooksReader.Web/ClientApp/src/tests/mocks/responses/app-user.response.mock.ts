import { AppUser } from '@br/core/models';
import { SiteRoles } from '@br/core/enums';

export const appUser  = {
    avatar:'',
    email:'',
    id:'',
    username: 'tests',
    roles: [SiteRoles.user, SiteRoles.author]
} as AppUser