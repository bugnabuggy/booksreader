import { SiteRoles } from '@br/core/enums';
import { Endpoints } from './endpoints';

export class MenuSections {
    static [SiteRoles.user] =  
    [
        {text: 'Profile', url: Endpoints.frontend.user.profileUrl },
    ]
    
    static [SiteRoles.reader] =  
    [
        {text: 'Reader profile', url: Endpoints.frontend.reader.profileUrl },
        {text: 'Reader Dashboard', url: Endpoints.frontend.reader.dashboardUrl },
    ]
    
    static [SiteRoles.author] =  
    [
        {text: 'Author profile', url: Endpoints.frontend.author.profileUrl },
        {text: 'Author Dashboard', url: Endpoints.frontend.author.dashboardUrl },
    ]

    static [SiteRoles.admin] =  
    [
        {text: 'Admin Dashboard', url: Endpoints.frontend.admin.dashboardUrl },
        {text: 'All users', url: Endpoints.frontend.admin.allUsersUrl },
    ]
    
}
