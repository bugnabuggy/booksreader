import { SiteRoles } from '@br/core/enums';
import { Endpoints } from './endpoints';

export class MenuSections {
    static [SiteRoles.user] =  
    [
        {text: 'Profile', url: Endpoints.forntend.user.profileUrl },
    ]
    
    static [SiteRoles.reader] =  
    [
        {text: 'Reader profile', url: Endpoints.forntend.reader.profileUrl },
        {text: 'Reader Dashboard', url: Endpoints.forntend.reader.dashboardUrl },
    ]
    
    static [SiteRoles.author] =  
    [
        {text: 'Author profile', url: Endpoints.forntend.author.profileUrl },
        {text: 'Author Dashboard', url: Endpoints.forntend.author.dashboardUrl },
    ]

    static [SiteRoles.admin] =  
    [
        {text: 'Admin Dashboard', url: Endpoints.forntend.admin.dashboardUrl },
        {text: 'All users', url: Endpoints.forntend.admin.allUsersUrl },
    ]
    
}