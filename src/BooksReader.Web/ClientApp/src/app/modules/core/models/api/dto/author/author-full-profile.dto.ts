import { AuthorProfile, PublicPage } from '../../entities';
import { UserDomain } from '../user-domain';

export interface AuthorFullProfile extends AuthorProfile {
    domains: UserDomain[];
    page: PublicPage;
}